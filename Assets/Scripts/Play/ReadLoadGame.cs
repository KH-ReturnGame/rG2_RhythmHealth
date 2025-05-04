using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

[System.Serializable]
public class GameData
{
    public GameSettings settings;
    public List<NoteAction> actions;
}

[System.Serializable]
public class GameSettings
{
    public string artist;
    public string artistLinks;
    public string song;
    public string author;
    public string previewIcon;
    public string levelDesc;
    public string levelTags;
    public int difficulty;
    public string songFile;
    public float bpm;
    public float damageRate;
    public float speed;
}

[System.Serializable]
public class NoteAction
{
    public string NoteType;
    public int Gym;
    public float beatsPerMinute;
    public float WaitBeat;
    public float LongTime;
    public int Multi;
}

public class ReadLoadGame : MonoBehaviour
{
    AudioSource audioSource;
    public NoteSpawn noteSpawn;
    public TextAsset jsonFile;
    public GameData gameData;
    float BPM;
    string WorkType;
    int WorkIndex = 0;
    string songPath;  
    public float offset;
    public bool isNotPrologue;
    
    void OnEnable()
    {
        offset = PlayerPrefs.GetInt("PlayerOffset");
        audioSource = GetComponent<AudioSource>();
        SetUp();
        StartCoroutine(PlayMusicWithOffset());
    }

    void SetUp()
    {
        if(isNotPrologue)
        {
            string song = PlayerPrefs.GetString("SongName");
            string jsonPath = Path.Combine("C:/RHRoutines", song, song + ".json");

            string jsonText = File.ReadAllText(jsonPath);
            gameData = JsonUtility.FromJson<GameData>(jsonText);
        }
        else
        {
            gameData = JsonUtility.FromJson<GameData>(jsonFile.text);
        }
        BPM = gameData.settings.bpm;
        songPath = Path.Combine(Application.streamingAssetsPath, gameData.settings.songFile);
    }

    IEnumerator PlayMusicWithOffset()
    {
        // string url = "file://" + songPath;
        // using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.UNKNOWN))
        // {
        //     yield return www.SendWebRequest();

        //     if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        //     {
        //         Debug.LogError("오디오 로드 오류: " + www.error);
        //     }
        //     else
        //     {
        //         AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
        //         audioSource.clip = clip;

        //         // 오프셋 적용
        //         yield return new WaitForSeconds(offset / 1000f);
        //         audioSource.Play();
        //         StartCoroutine(WorkRythm());
        //     }
        // }
        
        yield return new WaitForSeconds(offset / 1000f);
        StartCoroutine(WorkRythm());
    }

    IEnumerator WorkRythm()
    {
        while (WorkIndex < gameData.actions.Count)
        {
            WorkType = gameData.actions[WorkIndex].NoteType;
            if (WorkType == "Short")
            {
                noteSpawn.ShortNote();
            }
            else if (WorkType == "Long")
            {
                noteSpawn.LongNote(gameData.actions[WorkIndex].LongTime, BPM);
            }
            else if (WorkType == "Double")
            {
                noteSpawn.DoubleNote();
            }
            else if (WorkType == "Multi")
            {
                noteSpawn.MultiNote();
            }
            yield return new WaitForSeconds(gameData.actions[WorkIndex].WaitBeat * 60 / (BPM * gameData.settings.speed));
            Debug.Log(gameData.actions[WorkIndex].WaitBeat * 60 / (BPM * gameData.settings.speed));
            WorkIndex++;
        }
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        yield return null;
    }
}