using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public NoteVerdict noteVerdict;
    SpriteRenderer spriteRenderer;
    public Sprite[] note_ready;
    public Sprite[] note;    
    public Sprite[] note_fail;
    public Sprite[] longnote_ready;
    public Sprite[] longnote;
    public Sprite[] longnote_fail;
    public Sprite[] doublenote_ready;
    public Sprite[] doublenote;
    public Sprite[] doublenote_fail;
    public Sprite[] multinote_ready;
    public Sprite[] multinote_one;
    public Sprite[] multinote_two;
    public Sprite[] multinote_three;
    public Sprite[] multinote_four;
    public Sprite[] multinote_fail;
    bool? _is_Success = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _is_Success = noteVerdict.isSuccess;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _is_Success = noteVerdict.isSuccess;
        if(_is_Success != null)
        {
            ChangeSpriteNote();
            _is_Success = null;
        }

        if(noteVerdict.isInLong)
        {
            spriteRenderer.sprite = longnote[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
        }
    }

    void ChangeSpriteNote()
    {
        if(noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].NoteType == "Short")
        {
            if(_is_Success == true)
            {
                spriteRenderer.sprite = note[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
            }
            else if(_is_Success == false)
            {
                spriteRenderer.sprite = note_fail[0];
            }
        }
        else if(noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].NoteType == "Double")
        {
            if(_is_Success == true)
            {
                spriteRenderer.sprite = doublenote[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
            }
            else if(_is_Success == false)
            {
                spriteRenderer.sprite = doublenote_fail[0];
            }
        }
        else if(noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].NoteType == "Multi")
        {
            if(_is_Success == false)
            {
                spriteRenderer.sprite = multinote_fail[0];
            }
        }
        else if(noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].NoteType == "Long")
        {
            if(_is_Success == true)
            {
                spriteRenderer.sprite = longnote_ready[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
            }
            if(_is_Success == false)
            {
                spriteRenderer.sprite = longnote_fail[0];
            }
        }
    }

    public void ChangeSpriteMultiNote()
    {
        if(noteVerdict.multiNoteCount > 4)
        {
            noteVerdict.multiNoteCount = 1;
        }

        switch (noteVerdict.multiNoteCount)
        {
            case 1:
                spriteRenderer.sprite = multinote_one[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
                break;
            case 2:
                spriteRenderer.sprite = multinote_two[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
                break;
            case 3:
                spriteRenderer.sprite = multinote_three[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
                break;
            case 4:
                spriteRenderer.sprite = multinote_four[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
                break;
            default:
                break;
        }
        noteVerdict.multiNoteCount++;
    }
}