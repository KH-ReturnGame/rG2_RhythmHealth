using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
    public string artistPermission;
    public string artistLinks;
    public string song;
    public string author;
    public string previewImage;
    public string previewIcon;
    public string levelInfo;
    public string levelTags;
    public int difficulty;
    public string songFile;
    public float bpm;
    public float damageRate;
}

[System.Serializable]
public class NoteAction
{
    public string NoteType;
    public int Gym;
    public float beatsPerMinute;
    public float WaitBeat;
    public float LongTime;
}

public class ReadLoadGame : MonoBehaviour
{
    AudioSource audioSource;
    public NoteSpawn noteSpawn;
    public Animator player_work;
    public TextAsset jsonFile;
    public GameData gameData;
    float BPM;
    string WorkType;
    int WorkIndex = 0;
    string songPath;  
    public float offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = PlayerPrefs.GetInt("PlayerOffset");
        audioSource = GetComponent<AudioSource>();
        SetUp();
        StartCoroutine(PlayMusicWithOffset());
        StartCoroutine(WorkRythm());
    }

    void SetUp()
    {
        gameData = JsonUtility.FromJson<GameData>(jsonFile.text);
        BPM = gameData.settings.bpm;
        songPath = Path.Combine(Application.streamingAssetsPath, gameData.settings.songFile);
    }

    IEnumerator PlayMusicWithOffset()
    {
        using (WWW www = new WWW("file://" + songPath))
        {
            yield return www;
            audioSource.clip = www.GetAudioClip(false, false);
            
            // 오프셋 적용
            yield return new WaitForSeconds(offset / 1000f);
            audioSource.Play();
        }
    }

    IEnumerator WorkRythm()
    {
        if(WorkIndex > gameData.actions.Count - 1)
        {
            StartCoroutine(End());
            yield return null;
        }
        else
        {   
            WorkType = gameData.actions[WorkIndex].NoteType;
            if(WorkType == "Short")
            {
                noteSpawn.ShortNote();
            }
            else if(WorkType == "Long")
            {
                noteSpawn.LongNote(gameData.actions[WorkIndex].LongTime, BPM);
            }
            else if(WorkType == "Double")
            {
                noteSpawn.DoubleNote();
            }
            yield return new WaitForSeconds(gameData.actions[WorkIndex].WaitBeat * 60 / BPM);
            WorkIndex++;

            StartCoroutine(WorkRythm());

            yield return null;
        }
    }

    IEnumerator End()
    {
        yield return null;
    }
}
