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
    private AudioSource audioSource;
    private int sampleRate;
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
    float PlayWaitTime;
    bool isplaying = false;

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
            if (DataManager.Instance.isPre == false)
            {
                string song = PlayerPrefs.GetString("SongName");
                string jsonPath = Path.Combine("C:/RHRoutines", song, song + ".json");

                string jsonText = File.ReadAllText(jsonPath);
                gameData = JsonUtility.FromJson<GameData>(jsonText);
            }
            else
            {
                gameData = JsonUtility.FromJson<GameData>(DataManager.Instance.jsonData.text);
            }
        }
        else
        {
            gameData = JsonUtility.FromJson<GameData>(jsonFile.text);
        }
        BPM = gameData.settings.bpm;
        songPath = Path.Combine(Application.streamingAssetsPath, gameData.settings.songFile);
        PlayWaitTime = 6.25f / ((BPM / 60f) * gameData.settings.speed / 0.6f);
    }

    void Update()
    {
        if (isplaying)
        {
            double timeMs2 = (audioSource.timeSamples / (double)sampleRate) * 1000.0;
            Debug.Log("time : " + timeMs2);
            // SpawnNote(WorkIndex, gameData.actions[WorkIndex].NoteType);
            // WorkIndex++;
        }
    }

    IEnumerator ThreeCountDown()
    {
        StartCoroutine(LoadMusic());
        
        // 거리가 6.25f임 // 거리 = 속력 * 시간 => 시간 = 거리 / 속력 => 시간 = 6.25f / speed
        ThreeCount[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        ThreeCount[0].SetActive(false);
        ThreeCount[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        ThreeCount[1].SetActive(false);
        ThreeCount[2].SetActive(true);
        yield return new WaitForSeconds(1f);
        ThreeCount[2].SetActive(false);

        StartCoroutine(WorkRhythm());
        StartCoroutine(PlayWait());
    }

    IEnumerator LoadMusic()
    {
        if (isNotPrologue)
        {
            if (DataManager.Instance.isPre == false)
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
                }
            }
            else
            {
                audioSource.clip = DataManager.Instance.audioClip;
            }
        }
        else
        {
            ProlManager prolManager = GameObject.Find("ProlManager").GetComponent<ProlManager>();
            audioSource.clip = prolManager.playSongFiles[prolManager.index_PF];
        }
    }

    IEnumerator WorkRhythm()
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
            WorkIndex++;
            //Debug.Log(gameData.actions[WorkIndex].WaitBeat * 60 / (BPM * gameData.settings.speed));
        }
        StartCoroutine(End());
    }

    void SpawnNote(int i, string type)
    {
        if (i < gameData.actions.Count)
        {
            type = gameData.actions[i].NoteType;
            if (type == "Short")
            {
                noteSpawn.ShortNote();
            }
            else if (type == "Long")
            {
                noteSpawn.LongNote(gameData.actions[i].LongTime, BPM);
            }
            else if (type == "Double")
            {
                noteSpawn.DoubleNote();
            }
            else if (type == "Multi")
            {
                noteSpawn.MultiNote();
            }
        }
    }

    IEnumerator PlayWait()
    {
        Debug.Log("offset : " + offset);
        yield return new WaitForSeconds(PlayWaitTime + offset); // 오프셋 적용

        audioSource.Play();
        sampleRate = audioSource.clip.frequency;
        isplaying = true;
        yield return null;
    }

    IEnumerator End()
    {
        yield return null;
    }
}