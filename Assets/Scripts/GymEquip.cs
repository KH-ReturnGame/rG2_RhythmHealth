using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData_
{
    public GameSettings settings;
    public List<NoteAction> actions;
}

[System.Serializable]
public class GameSettings_
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
}

[System.Serializable]
public class NoteAction_
{
    public string NoteType;
    public int Gym;
    public float beatsPerMinute;
    public int WaitBeat;
}

public class GymEquip : MonoBehaviour
{
    public bool CanStart;
    public GameObject Lock;
    public GameObject Finteract;
    public GameObject StartPT_UI;
    public GameObject StartPT_Btn;
    private bool CanPT;
    public TextAsset jsonFile;    
    private GameData_ gameData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Lock.SetActive(!CanStart);
        gameData = JsonUtility.FromJson<GameData_>(jsonFile.text);
    }

    // Update is called once per frame
    void Update()
    {
        if(CanPT && Input.GetKey("f"))
        {
            StartPT_UI.SetActive(true);
            StartPT_Btn.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && CanStart)
        {
            Finteract.SetActive(true);
            CanPT = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && CanStart)
        {
            Finteract.SetActive(false);
            CanPT = false;
        }
    }
}
