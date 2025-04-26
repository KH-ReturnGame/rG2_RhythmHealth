using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BeatInfo : MonoBehaviour
{
    bool isBeat;
    public int Beat_index;
    string NoteType;
    int Gym;
    int WaitBeat;
    float LongTime;
    int Multi;
    public Dropdown NoteType_ID;
    public InputField Gym_ID;
    public InputField WaitBeat_ID;
    public InputField LongTime_ID;
    public InputField Multi_ID;
    private string[] NoteTypeList = { "Short", "Long", "Double", "Multi" };

    SaveInfo saveInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveInfo = GameObject.Find("SAVEBUTTON").GetComponent<SaveInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPointerClick(PointerEventData eventData)     
    {         
        if (eventData.button.Equals(PointerEventData.InputButton.Right))
        {             
            SetNoteType();
            Debug.Log("Right-click detected on " + gameObject.name);
        }
        else if (eventData.button.Equals(PointerEventData.InputButton.Left))
        {             
            NoteType_ID.value = System.Array.IndexOf(NoteTypeList, NoteType);
            Gym_ID.text = Gym.ToString();
            WaitBeat_ID.text = WaitBeat.ToString();
            LongTime_ID.text = LongTime.ToString();
            Multi_ID.text = Multi.ToString();
            Debug.Log("Left-click detected on " + gameObject.name);
        } 
    }

    public void SetNoteType()
    {
        if(isBeat == false)
        {    
            NoteType = NoteTypeList[NoteType_ID.value];
            isBeat = true;
        }
        else
        {
            NoteType_ID.value = System.Array.IndexOf(NoteTypeList, NoteType);
            Gym_ID.text = Gym.ToString();
            WaitBeat_ID.text = WaitBeat.ToString();
            LongTime_ID.text = LongTime.ToString();
            Multi_ID.text = Multi.ToString();
        }
    }
    public void SetGym()
    {
        Gym = int.Parse(Gym_ID.text);
    }
    public void SetWaitBeat()
    {
        WaitBeat = int.Parse(WaitBeat_ID.text);
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
        saveInfo.Save_NotesType(NoteType);
        saveInfo.Save_Gym(Gym_ID.text);
        saveInfo.Save_Multi(Multi_ID.text);
        saveInfo.Save_Long(LongTime_ID.text);
        saveInfo.Save_Wait(WaitBeat_ID.text);
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