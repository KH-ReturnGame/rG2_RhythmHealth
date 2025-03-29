using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Combo_UI : MonoBehaviour
{
    int temp;
    public NoteVerdict noteVerdict;
    TextMeshProUGUI Combo_text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Combo_text = GetComponentInChildren<TextMeshProUGUI>();
        temp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(noteVerdict.Combo > temp)
        {
            temp = noteVerdict.Combo;
            Debug.Log(noteVerdict.Combo);
            StartCoroutine(ComboUp());
        }
    }

    IEnumerator ComboUp()
    {
        Combo_text.text = temp.ToString();
        yield return null;
    }
}
