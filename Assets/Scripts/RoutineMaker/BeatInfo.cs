using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BeatInfo : MonoBehaviour
{
    public bool isBeat = false;
    public int Beat_index;

    public string NoteType;
    public int Gym;
    public int WaitBeat;
    public float LongTime;
    public int Multi;

    public TMP_Dropdown NoteType_ID;
    public InputField Gym_ID;
    public InputField LongTime_ID;
    public InputField Multi_ID;
    public string[] NoteTypeList = { "Short", "Long", "Double", "Multi" };

    public SaveInfo saveInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveInfo = GameObject.Find("SAVEBUTTON").GetComponent<SaveInfo>();
        NoteType_ID = GameObject.Find("Notetype").GetComponent<TMP_Dropdown>();
        Gym_ID = GameObject.Find("Gym").GetComponent<InputField>();        
        Multi_ID = GameObject.Find("Multi").GetComponent<InputField>();
        LongTime_ID = GameObject.Find("LongTime").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NoteClick()     
    {
        if(isBeat)
        {
            NoteType_ID.value = System.Array.IndexOf(NoteTypeList, NoteType);
            Gym_ID.text = Gym.ToString();
            LongTime_ID.text = LongTime.ToString();
            Multi_ID.text = Multi.ToString();
            saveInfo.selected_index = Beat_index;
        }
        else
        {   
            NoteType_ID.value = 0;
            Gym_ID.text = "";
            LongTime_ID.text = "";
            Multi_ID.text = "";
            saveInfo.selected_index = Beat_index;
        }
    }

    public void SetNoteType()
    { 
        NoteType = NoteTypeList[NoteType_ID.value];
    }
    public void SetGym()
    {
        Gym = int.Parse(Gym_ID.text);
    }
    public void SetLongtime()
    {
        LongTime = float.Parse(LongTime_ID.text);
    }
    public void SetMulti()
    {
        Multi = int.Parse(Multi_ID.text);
    }

    public void End_note()
    {
        isBeat = true;
        SetNoteType();
        SetGym();
        SetLongtime();
        SetMulti();
        Change_Color();
    }

    void Change_Color()
    {
        Image image = GetComponent<Image>();
        switch (NoteType)
        {
            case "Short":
                image.color = Color.green;
                break;
            case "Long":
                image.color = Color.blue;
                break;
            case "Double":
                image.color = Color.yellow;
                break;
            case "Multi":
                image.color = Color.red;
                break;
            default:
                image.color = Color.white;
                break;
        }
    }

}