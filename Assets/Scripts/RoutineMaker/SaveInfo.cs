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

    private string noteType;
    private int Gym;
    private float BPM;
    private float wait;
    private float longNote;
    private int multiNote;

    Beats beats;
    public int selected_index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beats = GameObject.Find("Beats").GetComponent<Beats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save_Artist(string temp)
    {
        temp = artist;
    }
    public void Save_ArtistLinks(string temp)
    {
        temp = artistLinks;
    }
    public void Save_Songname(string temp)
    {
        temp = song;
    }
    public void Save_Author(string temp)
    {
        temp = author;
    }
    public void Save_LevelDesc(string temp)
    {
        temp = levelDesc;
    }
    public void Save_levelTags(string temp)
    {
        temp = levelTags;
    }
    public void Save_Difficulty(string temp)
    {
        int temp_difficulty = int.Parse(temp);
        difficulty = temp_difficulty;
    }
    public void Save_BPM(string temp)
    {
        float temp_bpm = float.Parse(temp);
        bpm = temp_bpm;
    }
    public void Save_DamageRate(string temp)
    {
        float temp_damagerate = float.Parse(temp);
        damageRate = temp_damagerate;
    }
    public void Save_speed(string temp)
    {
        float temp_speed = float.Parse(temp);
        speed = temp_speed;
    }

//##########################################################################################################

    public void Save_bpm(string temp)
    {
        float temp_bpm = float.Parse(temp);
        BPM = temp_bpm;
    }
    public void Save_NotesType(string temp)
    {
        noteType = temp;
    }
    public void Save_Gym(string temp)
    {
        int temp_gym = int.Parse(temp);
        Gym = temp_gym;
    }
    public void Save_Multi(string temp)
    {
        int temp_multi = int.Parse(temp);
        multiNote = temp_multi;
    }
    public void Save_Long(string temp)
    {
        float temp_long = float.Parse(temp);
        longNote = temp_long;
    }
    public void Save_Wait(string temp)
    {
        float temp_wait = float.Parse(temp);
        wait = temp_wait;
    }  

    public void Complete_note()
    {
        if(selected_index != null)
        {
            beats.Complete_note(selected_index);
        }
        else
        {
            // Debug.LogWarning("노트 골라지지 않음"); // 노트가 선택되지 않았을 때 경고 메시지 출력
        }
    }

    public void Write_File() //찐_최종 마지막 파일 쓰기 메소드
    {
        // 저장할 파일 경로 지정
        string filePath = Path.Combine(Application.persistentDataPath, "SaveInfo.txt");

        // 저장할 데이터를 문자열로 구성 (각 항목을 개행 문자로 구분)
        string data = "Artist: " + artist + "\n" +
                      "Artist Permission: " + artistPermission + "\n" +
                      "Artist Links: " + artistLinks + "\n" +
                      "Song: " + song + "\n" +
                      "Author: " + author + "\n" +
                      "Preview Image: " + previewImage + "\n" +
                      "Preview Icon: " + previewIcon + "\n" +
                      "Level Description: " + levelDesc + "\n" +
                      "Level Tags: " + levelTags + "\n" +
                      "Difficulty: " + difficulty + "\n" +
                      "Song File: " + songFile + "\n" +
                      "BPM: " + bpm + "\n" +
                      "Damage Rate: " + damageRate + "\n" +
                      "Speed: " + speed + "\n";

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