using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public int WorkIndex = 0;
    string songPath;  
    public float offset;
    public bool isNotPrologue;
    public GameObject[] ThreeCount;

    void OnEnable()
    {
        offset = PlayerPrefs.GetFloat("PlayerOffset");
        audioSource = GetComponent<AudioSource>();
        SetUp();
        StartCoroutine(ThreeCountDown());
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

    IEnumerator ThreeCountDown()
    {
        ThreeCount[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        ThreeCount[0].SetActive(false);
        ThreeCount[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        ThreeCount[1].SetActive(false);
        ThreeCount[2].SetActive(true);
        yield return new WaitForSeconds(1f);
        ThreeCount[2].SetActive(false);
        if(offset >= 0)
        {
            StartCoroutine(PlayMusicWithOffsetPLUS());
        }
        else
        {
            StartCoroutine(WorkRythm());
            StartCoroutine(PlayMusicWithOffsetMINUS());
        }
    }

    IEnumerator PlayMusicWithOffsetPLUS()
    {
        if(isNotPrologue)
        {
            string uri = "file://" + songPath;
            using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(uri, AudioType.UNKNOWN))
            {
                yield return uwr.SendWebRequest();
                if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("오디오 로드 오류: " + uwr.error);
                    yield break;
                }
                AudioClip clip = DownloadHandlerAudioClip.GetContent(uwr);
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
        else
        {
            ProlManager prolManager = GameObject.Find("ProlManager").GetComponent<ProlManager>();
            audioSource.clip = prolManager.playSongFiles[prolManager.index_PF];
            audioSource.Play();
        }
        
        Debug.Log("offset : " + offset);
        yield return new WaitForSeconds(offset / 1000f); // 오프셋 적용
        StartCoroutine(WorkRythm());
    }

    IEnumerator PlayMusicWithOffsetMINUS()
    {
        Debug.Log("offset : " + offset);
        yield return new WaitForSeconds(offset / 1000f);
        if(isNotPrologue)
        {
            string uri = "file://" + songPath;
            using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(uri, AudioType.UNKNOWN))
            {
                yield return uwr.SendWebRequest();
                if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("오디오 로드 오류: " + uwr.error);
                    yield break;
                }
                AudioClip clip = DownloadHandlerAudioClip.GetContent(uwr);
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
        else
        {
            ProlManager prolManager = GameObject.Find("ProlManager").GetComponent<ProlManager>();
            audioSource.clip = prolManager.playSongFiles[prolManager.index_PF];
            audioSource.Play();
        }
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