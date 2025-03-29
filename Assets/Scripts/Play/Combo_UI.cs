using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Combo_UI : MonoBehaviour
{
    int temp;
    bool GameStart = false;
    public NoteVerdict noteVerdict;
    public GameObject Combo_Particle;
    TextMeshProUGUI Combo_text;
    Vector3 originalScale;  // 원래 크기 저장
    void Awake()
    {
        Combo_text = GetComponentInChildren<TextMeshProUGUI>();
        originalScale = Combo_text.transform.localScale;  // 원래 크기 저장
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
            GameStart = true;
        }
        if(GameStart & noteVerdict.Combo == 0)
        {
            temp = 0;
            StartCoroutine(ComboDown());
        }
    }

    IEnumerator ComboUp()
    {
        Combo_text.text = temp.ToString();
        // 크기 애니메이션 (튀어나오는 효과)
        yield return StartCoroutine(ScaleTextEffect());
    }

    IEnumerator ComboDown()
    {
        Combo_text.text = temp.ToString();

        yield return null;
    }

    IEnumerator ScaleTextEffect()
    {
        // 파티클 생성
        Instantiate(Combo_Particle, new Vector3(0, -5, 0), Quaternion.identity);

        // 텍스트 크기 크게 시작
        Combo_text.transform.localScale = originalScale * 1.5f;  
        
        float time = 0f;
        float duration = 0.2f;  // 애니메이션 시간

        // 원래 크기로 돌아오는 애니메이션
        while (time < duration)
        {
            Combo_text.transform.localScale = Vector3.Lerp(Combo_text.transform.localScale, originalScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // 애니메이션 완료 후 원래 크기로 정확하게 설정
        Combo_text.transform.localScale = originalScale;
    }
}
