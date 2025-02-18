using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int doubleNoteInputCount = 0;
    private float lastDoubleNoteTime = -1f;
    private float doubleNoteTimeWindow = 0.15f; // 두 개 입력 간 허용 시간 (0.15초)
    private int currentNoteIndex = -1;
    void Start()
    {
        judgeCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.anyKeyDown && notesInRange.Count > 0)
        {
            JudgeNote();
        }

        if (life <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Die());
        }

        List<GameObject> keys = new List<GameObject>(longNoteHoldTimes.Keys);
        foreach (var note in keys)
        {
            if (Input.anyKey) 
            {
                longNoteHoldTimes[note] += Time.deltaTime;
                Debug.Log(longNoteHoldTimes[note]);
            }
        }
    }

    private void JudgeNote()
    {
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
                currentNoteIndex = i; // 가장 가까운 노트의 인덱스 저장
            }
        }

        foreach (var note in notesInRange)
        {
            float distance = Mathf.Abs(note.transform.position.x - transform.position.x);
            if (distance < minDistance)
            {
                closestNote = note;
                minDistance = distance;
            }
        }

        if (closestNote.CompareTag("doublenote"))
        {
            float currentTime = Time.time;

            if (doubleNoteInputCount == 0)
            {
                lastDoubleNoteTime = currentTime;
                doubleNoteInputCount++;
                return; // 첫 입력은 기다림
            }
            else if (currentTime - lastDoubleNoteTime <= doubleNoteTimeWindow)
            {
                // 두 번째 입력이 일정 시간 내 들어옴 → 정상 판정
                Debug.Log("Excellent!");
                Score += 25;
                Accuracy += 100 / realLoadGame.gameData.actions.Count;
                Combo++;
                doubleNoteInputCount = 0;
            }
            else
            {
                // 두 번째 입력이 너무 늦음 → Miss
                Debug.Log("Miss! (더블노트 타이밍 벗어남)");
                Combo = 0;
                life -= 5f * realLoadGame.gameData.settings.damageRate;
                doubleNoteInputCount = 0;
            }
            notesInRange.Remove(closestNote);
            Destroy(closestNote);
        }
        else if(closestNote.CompareTag("note"))
        {
            if (minDistance <= judgeCollider.bounds.size.x * 0.25f)
            {
                Debug.Log("Excellent!");
                Score += 20;
                Accuracy += 100 / realLoadGame.gameData.actions.Count;
                Combo++;
            }
            else if (minDistance <= judgeCollider.bounds.size.x * 0.5f)
            {
                Debug.Log("Great!");
                Score += 15;
                Accuracy += 50 / realLoadGame.gameData.actions.Count;
                Combo++;
            }
            else
            {
                Debug.Log("Miss!");
                Score += 5;
                Combo = 0;
                life -= 5f * realLoadGame.gameData.settings.damageRate;
            }
            notesInRange.Remove(closestNote);
            Destroy(closestNote);
        }
    }

    void OnTriggerEnter2D(Collider2D other) //노트 어레이에 넣기
    {
        if (other.CompareTag("note") || other.CompareTag("longnote") || other.CompareTag("doublenote"))
        {
            notesInRange.Add(other.gameObject);

            if (other.CompareTag("longnote"))
            {
                longNoteHoldTimes[other.gameObject] = 0f;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("note") || other.CompareTag("doublenote"))
        {
            if (notesInRange.Contains(other.gameObject))
            {
                Debug.Log("Miss! (노트 놓침)");
                Combo = 0;
                life -= 5f * realLoadGame.gameData.settings.damageRate;

                notesInRange.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("longnoteout"))
        {
            float holdTime = longNoteHoldTimes[other.gameObject];
            float requiredTime = realLoadGame.gameData.actions[currentNoteIndex].LongTime;
            float holdPercentage = Mathf.Clamp01(holdTime / requiredTime);
            if (holdPercentage >= 0.9f)
            {
                Debug.Log("Excellent! (롱노트)");
                Score += 20;
                Accuracy += 100 / realLoadGame.gameData.actions.Count;
                Combo++;
            }
            else if (holdPercentage >= 0.5f)
            {
                Debug.Log("Great! (롱노트)");
                Score += 15;
                Accuracy += 50 / realLoadGame.gameData.actions.Count;
                Combo++;
            }
            else
            {
                Debug.Log("Miss! (롱노트 실패)");
                Score += 5;
                Combo = 0;
            }
            longNoteHoldTimes.Remove(other.gameObject);
            notesInRange.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }

    IEnumerator Die()
    {
        life++;
        yield return null;
    }
}