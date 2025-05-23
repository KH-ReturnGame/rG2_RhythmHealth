using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;

[Serializable]
public class Settings 
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

[Serializable]
public class ActionData 
{
    public string NoteType;
    public int Gym;
    public float beatsPerMinute;
    public float WaitBeat;
    public float LongTime;
    public int Multi;
}

[Serializable]
public class SaveData 
{
    public Settings settings;
    public List<ActionData> actions;
}

public class SaveInfo : MonoBehaviour
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
    //###################################################################################
    public Beats beats;
    public int selected_index;
    public Warn warn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beats = GameObject.Find("Beats").GetComponent<Beats>();
        warn = GameObject.Find("Canvas").GetComponent<Warn>();
        if(!Directory.Exists("C:/RHRoutines"))
        {
            Directory.CreateDirectory("C:/RHRoutines");
        }
    }

    public void Save_Artist(InputField temp)
    {
        artist = temp.text;
    }
    public void Save_ArtistLinks(InputField temp)
    {
        artistLinks = temp.text;
    }
    public void Save_Songname(InputField temp)
    {
        song = temp.text;
    }
    public void Save_Author(InputField temp)
    {
        author = temp.text;
    }
    public void Save_LevelDesc(InputField temp)
    {
        levelDesc = temp.text;
    }
    public void Save_levelTags(InputField temp)
    {
        levelTags = temp.text;
    }
    public void Save_Difficulty(InputField temp)
    {
        int temp_difficulty = int.Parse(temp.text);
        difficulty = temp_difficulty;
    }
    public void Save_BPM(InputField temp)
    {
        float temp_bpm = float.Parse(temp.text);
        bpm = temp_bpm;
    }
    public void Save_DamageRate(InputField temp)
    {
        float temp_damagerate = float.Parse(temp.text);
        damageRate = temp_damagerate;
    }
    public void Save_speed(InputField temp)
    {
        float temp_speed = float.Parse(temp.text);
        speed = temp_speed;
    }
    
    public void Save_PreviewIcon(GameObject temp)
    {
        StartCoroutine(BrowseIcon(temp));
    }
    IEnumerator BrowseIcon(GameObject temp)
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter(".jpg", ".png"));
        yield return FileBrowser.WaitForLoadDialog(
            FileBrowser.PickMode.Files,   // 파일만 선택
            false,                        // 다중 선택 비허용
            null, null,                   // 초기 경로·이름 없음
            "Select Image", "Open"        // 제목·버튼 텍스트
        );
        if( FileBrowser.Success )
        {
            previewIcon = FileBrowser.Result[0];
            Debug.Log($"Preview icon path saved: {previewIcon}");
            temp.SetActive(true);
        }
        else
        {
            Debug.Log("Image selection canceled.");
        }
        yield return null;
    }

    public void Save_SongFile(GameObject tempp)
    {
        StartCoroutine(BrowseSongFile(tempp));
    }
    IEnumerator BrowseSongFile(GameObject tempp)
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter(".mp3", ".wav"));
        yield return FileBrowser.WaitForLoadDialog(
            FileBrowser.PickMode.Files,   // 파일만 선택
            false,                        // 다중 선택 비허용
            null, null,                   // 초기 경로·이름 없음
            "Select music", "Open"        // 제목·버튼 텍스트
        );
        if( FileBrowser.Success )
        {
            songFile = FileBrowser.Result[0];
            Debug.Log($"music path saved: {songFile}");
            tempp.SetActive(true);
        }
        else
        {
            Debug.Log("music selection canceled.");
        }
        yield return null;
    }

    public void Complete_note() // 노트 하나 완료
    {
        if(beats.beatList.Count != 0)
        {
            beats.Complete_note(selected_index);
        }
        else
        {
            warn.make_Warn("노트가 선택되지 않았습니다.");
        }
    }

    [ContextMenu("To Json Data")]
    public void Write_File() //찐_최종 마지막 파일 쓰기 메소드
    {
        if(beats.beatList.Count == 0)
        {
            warn.make_Warn("노트가 하나도 선택되지 않았습니다.");
        }
        else if(artist == null || artistLinks == null || song == null || author == null || levelDesc == null || levelTags == null || difficulty == 0 || songFile == null || bpm == 0 || damageRate == 0 || speed == 0)
        {
            warn.make_Warn("정보가 모두 입력되지 않았습니다.");
        }
        else
        {
            Write_json();
        }
    }

    void Write_json()
    {
        Directory.CreateDirectory("C:/RHRoutines/" + song); // 디렉토리 생성
        // 노래파일, 미리보기 이미지지 복사 및 경로 변경
        File.Copy(songFile, "C:/RHRoutines/" + song + "/" + song + ".mp3", true);
        File.Copy(previewIcon, "C:/RHRoutines/" + song + "/" + song + ".png", true);
        songFile = "C:/RHRoutines/" + song + "/" + song + ".mp3"; // 노래파일 경로 변경
        previewIcon = "C:/RHRoutines/" + song + "/" + song + ".png"; // 미리보기 이미지 경로 변경

        var data = new SaveData();
        data.settings = new Settings {
            artist = artist,
            artistLinks = artistLinks,
            song = song,
            author = author,
            previewIcon = previewIcon,
            levelDesc = levelDesc,
            levelTags = levelTags,
            difficulty = difficulty,
            songFile = songFile,
            bpm = bpm,
            damageRate = damageRate,
            speed = speed
        };
        data.actions = new List<ActionData>();

        foreach (var beatObj in beats.beatList) {
            var info = beatObj.GetComponent<BeatInfo>();
            if (info == null || !info.isBeat) continue;

            var action = new ActionData {
                NoteType = info.NoteType, 
                Gym = info.Gym, 
                beatsPerMinute = bpm, 
                WaitBeat = info.WaitBeat, 
                LongTime = info.LongTime, 
                Multi = info.Multi};
            data.actions.Add(action);
        }

        string json = JsonUtility.ToJson(data, true); // JSON 변환

        string path = Path.Combine("C:/RHRoutines/" + song, song + ".json");
        File.WriteAllText(path, json); // JSON 파일로 저장
        Debug.Log("JSON 저장 완료: " + path);
    }
}