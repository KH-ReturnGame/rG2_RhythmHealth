using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrintResult : MonoBehaviour
{
    public NoteVerdict noteVerdict;
    private string grade;
    public TextMeshProUGUI _score;
    public TextMeshProUGUI _accuracy;
    public TextMeshProUGUI _combo;
    public TextMeshProUGUI _grade;
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
        _accuracy.text = noteVerdict.Accuracy.ToString("F1") + "%"; // 정확도를 소수점 1자리로 표시
        _score.text = noteVerdict.Score.ToString();                // 점수 설정
        _combo.text = noteVerdict.Combo.ToString();
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
        
        _grade.text = grade;
        
        yield return null;
    }
}
