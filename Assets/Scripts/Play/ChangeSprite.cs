using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public NoteVerdict noteVerdict;
    private SpriteRenderer spriteRenderer;

    // 노트 타입별 스프라이트 배열
    public Sprite[] note_ready;       // 일반 노트 준비 상태
    public Sprite[] longnote_ready;   // 롱노트 준비 상태
    public Sprite[] doublenote_ready; // 더블 노트 준비 상태
    public Sprite[] multinote_ready;  // 멀티 노트 준비 상태
    public Sprite[] note;             // 일반 노트 성공 상태
    public Sprite[] longnote;         // 롱노트 성공 상태
    public Sprite[] doublenote;       // 더블 노트 성공 상태
    public Sprite[] multinote;        // 멀티 노트 성공 상태
    public Sprite[] note_fail;        // 일반 노트 실패 상태
    public Sprite[] longnote_fail;    // 롱노트 실패 상태
    public Sprite[] doublenote_fail;  // 더블 노트 실패 상태
    public Sprite[] multinote_fail;   // 멀티 노트 실패 상태

    private string currentNoteTag = ""; // 현재 노트의 태그를 추적
    private bool isJudged = false;      // 판정이 완료되었는지 여부
    private bool isSuccess = false;     // 판정 성공 여부

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (noteVerdict.currentNoteIndex >= 0 && noteVerdict.currentNoteIndex < noteVerdict.realLoadGame.gameData.actions.Count)
        {
            UpdateSprite();
        }
    }

    void UpdateSprite()
    {
        // 현재 노트가 범위 안에 있는지 확인
        if (noteVerdict.notesInRange.Count > 0)
        {
            GameObject currentNote = noteVerdict.notesInRange[0]; // 가장 가까운 노트로 가정
            currentNoteTag = currentNote.tag;
            int gymIndex = noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym - 1;

            // 판정 전 준비 상태 스프라이트
            if (!isJudged)
            {
                switch (currentNoteTag)
                {
                    case "note":
                        spriteRenderer.sprite = note_ready[gymIndex];
                        break;
                    case "longnote":
                        spriteRenderer.sprite = longnote_ready[gymIndex];
                        break;
                    case "doublenote":
                        spriteRenderer.sprite = doublenote_ready[gymIndex];
                        break;
                    case "multinote":
                        spriteRenderer.sprite = multinote_ready[gymIndex];
                        break;
                }
            }
        }

        // 롱노트 진행 중일 때
        if (noteVerdict.isInLong && noteVerdict.currentLongNote != null)
        {
            int gymIndex = noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym - 1;
            spriteRenderer.sprite = longnote[gymIndex]; // 롱노트 진행 중 성공 상태로 표시
        }
    }

    // 판정 결과에 따라 스프라이트를 업데이트하는 메서드
    public void OnNoteJudged(bool success)
    {
        isJudged = true;
        isSuccess = success;
        int gymIndex = noteVerdict.realLoadGame.gameData.actions[noteVerdict.currentNoteIndex].Gym - 1;

        // 판정 결과에 따른 스프라이트 변경
        switch (currentNoteTag)
        {
            case "note":
                spriteRenderer.sprite = success ? note[gymIndex] : note_fail[gymIndex];
                break;
            case "longnote":
                spriteRenderer.sprite = success ? longnote[gymIndex] : longnote_fail[gymIndex];
                break;
            case "doublenote":
                spriteRenderer.sprite = success ? doublenote[gymIndex] : doublenote_fail[gymIndex];
                break;
            case "multinote":
                spriteRenderer.sprite = success ? multinote[gymIndex] : multinote_fail[gymIndex];
                break;
        }

        // 일정 시간 후 초기화 (판정 애니메이션 후)
        Invoke("ResetJudgement", 0.5f);
    }

    void ResetJudgement()
    {
        isJudged = false;
        isSuccess = false;
        currentNoteTag = "";
        spriteRenderer.sprite = null; // 기본 상태로 돌아감
    }

    // 노트가 범위에서 나갔을 때 호출
    public void OnNoteMissed()
    {
        if (!isJudged)
        {
            OnNoteJudged(false); // 실패로 처리
        }
    }
}