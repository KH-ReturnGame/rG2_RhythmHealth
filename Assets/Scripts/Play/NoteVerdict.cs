using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteVerdict : MonoBehaviour
{
    // 읽어와라
    private BoxCollider2D judgeCollider;
    public ReadLoadGame realLoadGame;
    public ChangeSprite changeSprite;

    public int Combo { get; private set; }
    public float Score { get; private set; }
    public float Accuracy { get; private set; }
    public float life = 100f;

    // 기본 노트 구조
    public List<GameObject> notesInRange = new List<GameObject>();
    private Dictionary<GameObject, float> longNoteHoldTimes = new Dictionary<GameObject, float>();
    
    // 더블 노트 관련 변수
    private int doubleNoteInputCount = 0;
    private float lastDoubleNoteTime = -1f;
    private float doubleNoteTimeWindow = 0.15f; // 더블 노트 입력 허용 시간
    
    // 롱노트 관련 변수
    public bool isInLong = false;
    public GameObject currentLongNote = null;
    private bool isKeyPressed = false; // 키가 눌려 있는지 여부
    private bool hadJudgedLong = false;
    
    // changeSprite 관련 변수
    public int currentNoteIndex = -1;
    public bool? isSuccess = null;
    public int multiNoteCount = 1;
    public GameObject Printer;

    void Start()
    {
        judgeCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // 모든 노트 타입을 Input.anyKey로 처리하고 말거야~
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
                    //Debug.Log($"Long Note Hold Time: {longNoteHoldTimes[currentLongNote]}");
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

        // 사망
        if (life <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Die());
        }

        // 결과 출력
        if(currentNoteIndex + 1 == realLoadGame.gameData.actions.Count)
        {
            StartCoroutine(Result());
        }
    }

    private void JudgeNote()
    {
        if (notesInRange.Count == 0) return;

        // 가장 가까운 노트 찾기
        GameObject closestNote = notesInRange[0];
        float minDistance = Mathf.Abs(closestNote.transform.position.x - transform.position.x);

        for (int i = 1; i < notesInRange.Count; i++)
        {
            float distance = Mathf.Abs(notesInRange[i].transform.position.x - transform.position.x);
            if (distance < minDistance)
            {
                closestNote = notesInRange[i];
                minDistance = distance;
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
                isSuccess = true;
            }
            else if (minDistance <= judgeCollider.bounds.size.x * 0.5f)
            {
                Debug.Log("Great!");
                Score += 15;
                Accuracy += 50 / realLoadGame.gameData.actions.Count;
                Combo++;
                isSuccess = true;
            }
            else
            {
                Debug.Log("Miss!");
                Score += 5;
                Combo = 0;
                life -= 5f * realLoadGame.gameData.settings.damageRate;
                isSuccess = false;
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
                return; // 첫 번째 입력 기다림
            }
            else if (currentTime - lastDoubleNoteTime <= doubleNoteTimeWindow)
            {
                Debug.Log("Excellent! (Double Note)");
                Score += 25;
                Accuracy += 100 / realLoadGame.gameData.actions.Count;
                Combo++;
                isSuccess = true;
                doubleNoteInputCount = 0;
                notesInRange.Remove(closestNote);
                Destroy(closestNote);
            }
            else
            {
                Debug.Log("Miss! (Double Note timing out)");
                Combo = 0;
                life -= 5f * realLoadGame.gameData.settings.damageRate;
                isSuccess = false;
                doubleNoteInputCount = 0;
            }
        }
        else if (closestNote.CompareTag("multinote"))
        {
            // 멀티 노트 판정
            if (realLoadGame.gameData.actions[currentNoteIndex].Multi > 0)
            {
                realLoadGame.gameData.actions[currentNoteIndex].Multi -= 1; // 남은 횟수 감소
                changeSprite.ChangeSpriteMultiNote();
                Debug.Log($"Multi Note: 남은 횟수 - {realLoadGame.gameData.actions[currentNoteIndex].Multi}");
                // TextMeshPro 텍스트 업데이트
                Combo++;
                UpdateMultiNoteText(closestNote, realLoadGame.gameData.actions[currentNoteIndex].Multi);
            }
        }
        else if (closestNote.CompareTag("longnote"))
        {
            isInLong = true;
            hadJudgedLong = false;
            currentLongNote = closestNote;
            if (!longNoteHoldTimes.ContainsKey(currentLongNote))
            {
                longNoteHoldTimes[currentLongNote] = 0f;
            }
        }
    }

    // 롱노트 판정 메서드
    private void JudgeLongNote()
    {
        if (currentLongNote != null && notesInRange.Contains(currentLongNote))
        {
            float holdTime = longNoteHoldTimes.ContainsKey(currentLongNote) ? longNoteHoldTimes[currentLongNote] : 0f;
            float requiredTime = realLoadGame.gameData.actions[currentNoteIndex].LongTime * (60 / realLoadGame.gameData.actions[currentNoteIndex].beatsPerMinute);
            float speedRate = realLoadGame.gameData.settings.speed;
            float holdPercentage = Mathf.Clamp01(holdTime / (requiredTime / speedRate));

            if (holdPercentage >= 0.9f)
            {
                Debug.Log(holdTime);
                Debug.Log(requiredTime);
                Debug.Log(holdPercentage);
                Debug.Log("Excellent! (Long Note)");
                Score += 20;
                Accuracy += 100 / realLoadGame.gameData.actions.Count;
                Combo++;
                isSuccess = true;
            }
            else if (holdPercentage >= 0.5f)
            {
                Debug.Log(holdTime);
                Debug.Log(requiredTime);
                Debug.Log(holdPercentage);
                Debug.Log("Great! (Long Note)");
                Score += 15;
                Accuracy += 50 / realLoadGame.gameData.actions.Count;
                Combo++;
                isSuccess = true;
            }
            else
            {
                Debug.Log(holdTime);
                Debug.Log(requiredTime);
                Debug.Log(holdPercentage);
                Debug.Log("Miss! (Long Note failed)");
                Score += 5;
                Combo = 0;
                life -= 5f * realLoadGame.gameData.settings.damageRate;
                isSuccess = false;
            }
            longNoteHoldTimes.Remove(currentLongNote);
            notesInRange.Remove(currentLongNote);
            Destroy(currentLongNote);
            currentLongNote = null;
        }
        else
        {
            Debug.Log("Miss! (Long Note failed)");
            Score += 5;
            Combo = 0;
            life -= 5f * realLoadGame.gameData.settings.damageRate;
            isSuccess = false;
        }
        isInLong = false;
        hadJudgedLong = true;
    }

    // TextMeshPro 텍스트를 업데이트하는 메서드
    private void UpdateMultiNoteText(GameObject multiNote, int remainingHits)
    {
        // 자식 오브젝트에서 TextMeshPro 컴포넌트 찾기
        TextMeshPro tmp = multiNote.GetComponentInChildren<TextMeshPro>();
        if (tmp != null)
        {
            tmp.text = remainingHits.ToString(); // 남은 횟수를 텍스트로 표시
        }
        else
        {
            Debug.LogWarning("멀티노트의 자식 오브젝트에 TextMeshPro 컴포넌트가 없습니다ㅋ.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("note") || other.CompareTag("longnote") || other.CompareTag("doublenote") || other.CompareTag("multinote"))
        {
            notesInRange.Add(other.gameObject);

            if (other.CompareTag("longnote"))
            {
                longNoteHoldTimes[other.gameObject] = 0f;
                hadJudgedLong = false;
            }
            currentNoteIndex += 1;
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
                isSuccess = false;
            }
        }
        else if (other.CompareTag("multinote"))
        {
            if (notesInRange.Contains(other.gameObject))
            {
                if (realLoadGame.gameData.actions[currentNoteIndex].Multi <= 0)
                {
                    Debug.Log("Excellent! (Multi Note)");
                    Score += 20; // 멀티노트 성공 시 점수 추가
                    Accuracy += 100 / realLoadGame.gameData.actions.Count;
                    Combo++; // 성공 시 콤보 증가
                    isSuccess = true;
                }
                else
                {
                    Debug.Log("Miss! (Multi Note failed)");
                    Score += 5;
                    Combo = 0; // 실패 시 콤보 초기화
                    life -= 5f * realLoadGame.gameData.settings.damageRate;
                    isSuccess = false;
                }
                notesInRange.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("longnoteout"))
        {
            if(hadJudgedLong == false)
            {
                JudgeLongNote();
            }
            else
            {
                hadJudgedLong = true;
            }
        }
    }

    IEnumerator Die()
    {
        life++;
        yield return new WaitForSeconds(1.5f);
        Printer.SetActive(true);
        yield return null;
    }

    IEnumerator Result()
    {
        yield return new WaitForSeconds(1f);
        Printer.SetActive(true);
        yield return null;
    }
}