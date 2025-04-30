using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class Setting
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
    public int bpm;
    public int damageRate;
    public float speed;
}

[Serializable]
public class ActionDatas
{
    public string NoteType;
    public int Gym;
    public int beatsPerMinute;
    public int WaitBeat;
    public int LongTime;
    public int Multi;
}

[Serializable]
public class RoutineData
{
    public Setting settings;
    public ActionDatas[] actions;
}

public class EnterGame : MonoBehaviour
{
    [SerializeField]
    private List<RoutineData> routines = new List<RoutineData>();
    public Image icon;
    public TextMeshProUGUI SongName;
    public TextMeshProUGUI SongDesc;
    public int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var file in Directory.GetFiles("C:/RHRoutines", "*.json", SearchOption.AllDirectories)) 
        {
            string json = File.ReadAllText(file);
            routines.Add(JsonUtility.FromJson<RoutineData>(json));
        }
        index = 0;
        LoadData();
    }

    public void LoadData()
    {   
        var data = routines[index];
        SongName.text = data.settings.song;
        SongDesc.text = data.settings.levelDesc;
        string spriteName = Path.GetFileNameWithoutExtension(data.settings.previewIcon);
        icon.sprite = Resources.Load<Sprite>(spriteName);
    }

    public void RightData()
    {
        index++;
        if (index >= routines.Count)
        {
            index = 0;
        }
        LoadData();
    }

    public void LeftData()
    {
        index--;
        if (index < 0)
        {
            index = routines.Count - 1;
        }
        LoadData();
    }
}
