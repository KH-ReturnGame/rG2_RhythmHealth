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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)     
    {         
        if (eventData.button.Equals(PointerEventData.InputButton.Right))         
        {             
            SetNoteType();
        }
        else if (eventData.button.Equals(PointerEventData.InputButton.Left))         
        {             
            NoteType_ID.value = System.Array.IndexOf(NoteTypeList, NoteType);
            Gym_ID.text = Gym.ToString();
            WaitBeat_ID.text = WaitBeat.ToString();
            LongTime_ID.text = LongTime.ToString();
            Multi_ID.text = Multi.ToString();
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

}