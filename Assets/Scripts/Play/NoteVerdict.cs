using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteVerdict : MonoBehaviour
{
    private BoxCollider2D judgeCollider;
    public ReadLoadGame realLoadGame;

    public int Combo { get; private set; }
    public float Score { get; private set; }
    public float Accuracy { get; private set; }
    public float life = 100f;

    private List<GameObject> notesInRange = new List<GameObject>();
    private Dictionary<GameObject, float> longNoteHoldTimes = new Dictionary<GameObject, float>();

    private bool isInLong = false;
    private GameObject currentLongNote = null;
    private bool isKeyPressed = false; // 키가 눌려 있는지 여부
    private int currentNoteIndex = -1;

    void Start()
    {
        judgeCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // 노트 입력 처리 (일반 노트)
        if (Input.anyKeyDown && notesInRange.Count > 0)
        {
            JudgeNote();
        }

        // 롱노트 처리 및 키 상태 추적
        if (isInLong)
        {
            if (Input.anyKey)
            {
                isKeyPressed = true;
                if (longNoteHoldTimes.ContainsKey(currentLongNote))
                {
                    longNoteHoldTimes[currentLongNote] += Time.deltaTime;
                    Debug.Log($"Long Note Hold Time: {longNoteHoldTimes[currentLongNote]}");
                }
            }
            else
            {
                if (isKeyPressed) // 키가 떼어진 순간
                {
                    JudgeLongNote();
                }
                isKeyPressed = false;
            }
        }

        if (life <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Die());
        }
    }

    private void JudgeNote()
    {
        if (notesInRange.Count == 0) return;

        // 가장 가까운 노트 찾기
        GameObject closestNote = notesInRange[0];
        float minDistance = Mathf.Abs(closestNote.transform.position.x - transform.position.x);
        currentNoteIndex = 0;

        for (int i = 1; i < notesInRange.Count; i++)
        {
            float distance = Mathf.Abs(notesInRange[i].transform.position.x - transform.position.x);
            if (distance < minDistance)
            {
                closestNote = notesInRange[i];
                minDistance = distance;
                currentNoteIndex = i;
            }
        }

        // 롱노트 시작
        if (closestNote.CompareTag("longnote"))
        {
            isInLong = true;
            currentLongNote = closestNote;
            if (!longNoteHoldTimes.ContainsKey(currentLongNote))
            {
                longNoteHoldTimes[currentLongNote] = 0f;
            }
        }
        // 기타 노트 처리 (일반 노트, 더블 노트 등 생략)
    }

    private void JudgeLongNote()
    {
        if (currentLongNote != null && notesInRange.Contains(currentLongNote))
        {
            float holdTime = longNoteHoldTimes.ContainsKey(currentLongNote) ? longNoteHoldTimes[currentLongNote] : 0f;
            float requiredTime = realLoadGame.gameData.actions[currentNoteIndex].LongTime * 60 / realLoadGame.gameData.actions[currentNoteIndex].beatsPerMinute;
            float holdPercentage = Mathf.Clamp01(holdTime / requiredTime);

            if (holdPercentage >= 0.9f)
            {
                Debug.Log("Excellent! (Long Note)");
                Score += 20;
                Accuracy += 100 / realLoadGame.gameData.actions.Count;
                Combo++;
            }
            else if (holdPercentage >= 0.5f)
            {
                Debug.Log("Great! (Long Note)");
                Score += 15;
                Accuracy += 50 / realLoadGame.gameData.actions.Count;
                Combo++;
            }
            else
            {
                Debug.Log(holdPercentage);
                Debug.Log("Miss! (Long Note failed)");
                Score += 5;
                Combo = 0;
                life -= 5f * realLoadGame.gameData.settings.damageRate;
            }
            longNoteHoldTimes.Remove(currentLongNote);
            notesInRange.Remove(currentLongNote);
            Destroy(currentLongNote);
            currentLongNote = null;
        }
        isInLong = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("longnote"))
        {
            notesInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("longnoteout"))
        {
            if (isInLong && currentLongNote != null && notesInRange.Contains(currentLongNote))
            {
                JudgeLongNote();
            }
        }
    }

    IEnumerator Die()
    {
        life++;
        yield return null;
    }
}