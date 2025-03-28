using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public NoteVerdict noteVerdict;
    SpriteRenderer spriteRenderer;
    public Sprite[] note_ready;
    public Sprite[] longnote_ready;
    public Sprite[] doublenote_ready;
    public Sprite[] multinote_ready;
    public Sprite[] note;
    public Sprite[] longnote;
    public Sprite[] doublenote;
    public Sprite[] multinote;
    public Sprite[] note_fail;
    public Sprite[] longnote_fail;
    public Sprite[] doublenote_fail;
    public Sprite[] multinote_fail;
    bool _is_Success;
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
        ChangeSpriteNote();
    }

    void ChangeSpriteNote()
    {
        if(note)
        {
            if(_is_Success)
            {
                spriteRenderer.sprite = note[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
            }
            else
            {
                spriteRenderer.sprite = note_fail[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
            }
        }
        else if(noteVerdict.isInLong)
        {
            spriteRenderer.sprite = note[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
        }
        else if(doublenote)
        {
            if(_is_Success)
            {
                spriteRenderer.sprite = doublenote[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
            }
            else
            {
                spriteRenderer.sprite = doublenote_fail[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
            }
        }
        else if(multinote)
        {
            if(_is_Success)
            {
                spriteRenderer.sprite = multinote[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
            }
            else
            {
                spriteRenderer.sprite = multinote_fail[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
            }
        }
    }   
}