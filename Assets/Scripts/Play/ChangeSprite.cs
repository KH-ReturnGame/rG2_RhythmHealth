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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpriteNote();
    }

    void ChangeSpriteNote()
    {
        if(noteVerdict.isInLong)
        {
            spriteRenderer.sprite = note[noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym -1];
        }
    }
    
}
