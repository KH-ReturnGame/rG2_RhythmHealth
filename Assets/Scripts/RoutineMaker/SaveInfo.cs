using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
public class SaveInfo : MonoBehaviour
{
    private string artist;
    private string artistPermission;
    private string artistLinks;
    private string song;
    private string author;
    private string previewImage;
    private string previewIcon;
    private string levelDesc;
    private string levelTags;
    private int difficulty;
    private string songFile;
    private float bpm;
    private float damageRate;
    private float speed;

    // private string noteType;
    // private int Gym;
    // private float BPM;
    // private float wait;
    // private float longNote;
    // private int multiNote;

    Beats beats;
    public int selected_index;
    Warn warn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beats = GameObject.Find("Beats").GetComponent<Beats>();
        warn = GameObject.Find("Canvas").GetComponent<Warn>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

//##########################################################################################################

    // public void Save_bpm(string temp)
    // {
    //     float temp_bpm = float.Parse(temp);
    //     BPM = temp_bpm;
    // }
    // public void Save_NotesType(string temp)
    // {
    //     noteType = temp;
    // }
    // public void Save_Gym(string temp)
    // {
    //     int temp_gym = int.Parse(temp);
    //     Gym = temp_gym;
    // }
    // public void Save_Multi(string temp)
    // {
    //     int temp_multi = int.Parse(temp);
    //     multiNote = temp_multi;
    // }
    // public void Save_Long(string temp)
    // {
    //     float temp_long = float.Parse(temp);
    //     longNote = temp_long;
    // }
    // public void Save_Wait(string temp)
    // {
    //     float temp_wait = float.Parse(temp);
    //     wait = temp_wait;
    // }  

    public void Complete_note()
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

    public void Write_File() //찐_최종 마지막 파일 쓰기 메소드
    {
        if(selected_index == null)
        {
            warn.make_Warn("노트가 하나도 선택되지 않았습니다.");
        }
        else if(artist == null || artistPermission == null || artistLinks == null || song == null || author == null || levelDesc == null || levelTags == null || difficulty == 0 || songFile == null || bpm == 0 || damageRate == 0 || speed == 0)
        {
            warn.make_Warn("정보가 모두 입력되지 않았습니다.");
        }
        else
        {
            // 저장할 파일 경로 지정
            string filePath = Path.Combine(Application.persistentDataPath, "SaveInfo.txt");

            // 저장할 데이터를 문자열로 구성 (각 항목을 개행 문자로 구분)
            string data = "{" + "\n" +
                          "settings:" + "\n" +
                          "{" + "\n" +
                          "artist: " + artist + "\n" +
                          "artistPermission: " + artistPermission + "\n" +
                          "artistLinks: " + artistLinks + "\n" +
                          "song: " + song + "\n" +
                          "author: " + author + "\n" +
                          "previewImage: " + previewImage + "\n" +
                          "previewIcon: " + previewIcon + "\n" +
                          "levelDesc: " + levelDesc + "\n" +
                          "levelTags: " + levelTags + "\n" +
                          "difficulty: " + difficulty + "\n" +
                          "songFile: " + songFile + "\n" +
                          "bpm: " + bpm + "\n" +
                          "damageRate: " + damageRate + "\n" +
                          "speed: " + speed + "\n" +
                          "}," + "\n" +
                          "actions:" + "\n" +
                          "[" + "\n" +
                          "]" + "\n" +
                          "}" + "\n";

            try 
            {
                // 파일에 데이터를 기록합니다.
                File.WriteAllText(filePath, data);
                Debug.Log("파일 저장 완료: " + filePath);
            }
            catch(System.Exception e)
            {
                Debug.LogError("파일 저장 실패: " + e.Message);
            }
        }
    }
    
}