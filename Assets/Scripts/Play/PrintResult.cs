using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintResult : MonoBehaviour
{
    public NoteVerdict noteVerdict;
    private string grade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Result());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Result()
    {
        Debug.Log(noteVerdict.Accuracy);
        Debug.Log(noteVerdict.Score);
        Debug.Log(noteVerdict.Combo);
        if(noteVerdict.Accuracy == 100)
        {
            grade = "S+";
        }
        else if(noteVerdict.Accuracy > 95)
        {
            grade = "S";
        }
        else if(noteVerdict.Accuracy > 80)
        {
            grade = "A";
        }
        else if(noteVerdict.Accuracy > 70)
        {
            grade = "B";
        }
        else if(noteVerdict.Accuracy > 55)
        {
            grade = "C";
        }
        else if(noteVerdict.Accuracy > 40)
        {
            grade = "D";
        }
        else if(noteVerdict.Accuracy > 20)
        {
            grade = "E";
        }
        else
        {
            grade = "F";
        }

        Debug.Log(grade);
        yield return null;
    }
}
