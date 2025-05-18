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
    public float WaitBeat;
    public float LongTime;
    public int Multi;

    public TMP_Dropdown NoteType_ID;
    public InputField Gym_ID;
    public InputField LongTime_ID;
    public InputField Multi_ID;
    public string[] NoteTypeList = { "Short", "Long", "Double", "Multi" };

    public SaveInfo saveInfo;
    public Beats beats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveInfo = GameObject.Find("SAVEBUTTON").GetComponent<SaveInfo>();
        beats = GameObject.Find("Beats").GetComponent<Beats>();
        NoteType_ID = GameObject.Find("Notetype").GetComponent<TMP_Dropdown>();
        Gym_ID = GameObject.Find("Gym").GetComponent<InputField>();        
        Multi_ID = GameObject.Find("Multi").GetComponent<InputField>();
        LongTime_ID = GameObject.Find("LongTime").GetComponent<InputField>();
    }

    public void NoteClick()     
    {
        if(isBeat)
        {
            NoteType_ID.value = System.Array.IndexOf(NoteTypeList, NoteType);
            Gym_ID.text = Gym.ToString();
            LongTime_ID.text = LongTime.ToString();
            Multi_ID.text = Multi.ToString();
        }
        else
        {   
            NoteType_ID.value = 0;
            Gym_ID.text = "";
            LongTime_ID.text = "";
            Multi_ID.text = "";
        }
        saveInfo.selected_index = Beat_index;
    }

    public void SetNoteType()
    { 
        NoteType = NoteTypeList[NoteType_ID.value];
    }
    public void SetGym()
    {
        if(Gym_ID.text == "")
            Gym = 1;
        else
            Gym = int.Parse(Gym_ID.text);
    }
    public void SetLongtime()
    {
        if(LongTime_ID.text == "")
            LongTime = 0;
        else
            LongTime = float.Parse(LongTime_ID.text);
    }
    public void SetMulti()
    {
        if(Multi_ID.text == "")
            Multi = 0;
        else
            Multi = int.Parse(Multi_ID.text);
    }
    public void SetWait()
    {
        WaitBeat = 0;
        for(int i = Beat_index + 1; i < beats.beatList.Count; i++)
        {
            WaitBeat++;
            if (beats.beatList[i].GetComponent<BeatInfo>().isBeat == true)
            {
                WaitBeat = WaitBeat / 4;
                break;
            }
        }
    }

    public void End_note()
    {
        isBeat = true;
        SetNoteType();
        SetGym();
        SetLongtime();
        SetMulti();
        for(int i = 0; i < beats.beatList.Count; i++)
        {
            beats.beatList[i].GetComponent<BeatInfo>().SetWait();
        }
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