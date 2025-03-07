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

    private int doubleNoteInputCount = 0;
    private float lastDoubleNoteTime = -1f;
    private float doubleNoteTimeWindow = 0.15f; // 더블 노트 입력 허용 시간
    private int currentNoteIndex = -1;

    // 롱노트 상태 관리 변수
    private bool isInLong = false;
    private GameObject currentLongNote = null;

    void Start()
    {
        judgeCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // 노트 입력 처리
        if (Input.anyKeyDown && notesInRange.Count > 0)
        {
            JudgeNote();
        }

        // 롱노트 시간 누적
        if (isInLong && Input.anyKey)
        {
            if (longNoteHoldTimes.ContainsKey(currentLongNote))
            {
                longNoteHoldTimes[currentLongNote] += Time.deltaTime;
                Debug.Log($"Long Note Hold Time: {longNoteHoldTimes[currentLongNote]}");
            }
        }

        // 키를 떼면 롱노트 판정
        if (isInLong && Input.anyKeyUp)
        {
            JudgeLongNote();
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

        // 노트 타입에 따른 판정
        if (closestNote.CompareTag("note"))
        {
            // 일반 노트 판정
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
        else if (closestNote.CompareTag("doublenote"))
        {
            // 더블 노트 판정
            float currentTime = Time.time;

            if (doubleNoteInputCount == 0)
            {
                lastDoubleNoteTime = currentTime;
                doubleNoteInputCount++;
                Debug.Log("Double Note: First input detected");
                return;
            }
            else if (currentTime - lastDoubleNoteTime <= doubleNoteTimeWindow)
            {
                Debug.Log("Excellent! (Double Note)");
                Score += 25;
                Accuracy += 100 / realLoadGame.gameData.actions.Count;
                Combo++;
                doubleNoteInputCount = 0;
                notesInRange.Remove(closestNote);
                Destroy(closestNote);
            }
            else
            {
                Debug.Log("Miss! (Double Note timing out)");
                Combo = 0;
                life -= 5f * realLoadGame.gameData.settings.damageRate;
                doubleNoteInputCount = 0;
            }
        }
        else if (closestNote.CompareTag("longnote"))
        {
            // 롱노트 시작
            isInLong = true;
            currentLongNote = closestNote;
            if (!longNoteHoldTimes.ContainsKey(currentLongNote))
            {
                longNoteHoldTimes[currentLongNote] = 0f;
            }
        }
        else if (closestNote.CompareTag("multinote"))
        {
            // 멀티 노트 판정
            int multiIndex = notesInRange.IndexOf(closestNote);
            if (multiIndex >= 0 && multiIndex < realLoadGame.gameData.actions.Count)
            {
                var action = realLoadGame.gameData.actions[multiIndex];
                if (action.Multi > 0)
                {
                    action.Multi -= 1;
                    Debug.Log($"Multi Note: 남은 횟수 - {action.Multi}");
                    UpdateMultiNoteText(closestNote, action.Multi);
                }
            }
        }
    }

    // 롱노트 판정 메서드
    private void JudgeLongNote()
    {
        if (currentLongNote != null && notesInRange.Contains(currentLongNote))
        {
            float holdTime = longNoteHoldTimes.ContainsKey(currentLongNote) ? longNoteHoldTimes[currentLongNote] : 0f;
            float requiredTime = realLoadGame.gameData.actions[currentNoteIndex].LongTime;
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

    private void UpdateMultiNoteText(GameObject multiNote, int remainingHits)
    {
        TextMeshPro tmp = multiNote.GetComponentInChildren<TextMeshPro>();
        if (tmp != null)
        {
            tmp.text = remainingHits.ToString();
        }
        else
        {
            Debug.LogWarning("멀티노트의 자식 오브젝트에 TextMeshPro 컴포넌트가 없습니다.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("note") || other.CompareTag("longnote") || other.CompareTag("doublenote") || other.CompareTag("multinote"))
        {
            notesInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("note") || other.CompareTag("doublenote"))
        {
            if (notesInRange.Contains(other.gameObject))
            {
                Debug.Log("Miss! (Note missed)");
                Combo = 0;
                life -= 5f * realLoadGame.gameData.settings.damageRate;
                notesInRange.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("multinote"))
        {
            if (notesInRange.Contains(other.gameObject))
            {
                int multiIndex = notesInRange.IndexOf(other.gameObject);
                if (multiIndex >= 0 && multiIndex < realLoadGame.gameData.actions.Count)
                {
                    if (realLoadGame.gameData.actions[multiIndex].Multi <= 0)
                    {
                        Debug.Log("Excellent! (Multi Note)");
                        Score += 20;
                        Accuracy += 100 / realLoadGame.gameData.actions.Count;
                        Combo++;
                    }
                    else
                    {
                        Debug.Log("Miss! (Multi Note failed)");
                        Score += 5;
                        Combo = 0;
                        life -= 5f * realLoadGame.gameData.settings.damageRate;
                    }
                    notesInRange.Remove(other.gameObject);
                    Destroy(other.gameObject);
                }
            }
        }
        else if (other.CompareTag("longnoteout"))
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