using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int WaitBeat;
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
    public string NoteType;
    public int Gym;
    public float beatsPerMinute;
    public int WaitBeat;
    public float LongTime;
    public int Multi;
    //###################################################################################
    public Beats beats;
    public int selected_index;
    public Warn warn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beats = GameObject.Find("Beats").GetComponent<Beats>();
        warn = GameObject.Find("Canvas").GetComponent<Warn>();
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

    public void Complete_note() // 노트 하나 완료료
    {
        if(selected_index != null)
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
        if(selected_index == null)
        {
            warn.make_Warn("노트가 하나도 선택되지 않았습니다.");
        }
        else if(artist == null || artistLinks == null || song == null || author == null || levelDesc == null || levelTags == null || difficulty == 0 || songFile == null || bpm == 0 || damageRate == 0 || speed == 0)
        {
            warn.make_Warn("정보가 모두 입력되지 않았습니다.");
        }
        else
        {
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
                    Multi = info.Multi
                };
                data.actions.Add(action);
            }
    
            // JSON 변환 및 저장
            string json = JsonUtility.ToJson(data, true);
            string path = Path.Combine(Application.persistentDataPath, song + ".json");
            File.WriteAllText(path, json);
            Debug.Log("JSON 저장 완료: " + path);
        }
    }
}