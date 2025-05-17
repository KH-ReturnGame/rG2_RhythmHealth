using System.Collections;
using UnityEngine;

public class NoteSpawn : MonoBehaviour
{
    public GameObject Short;
    public GameObject longNoteStartPrefab;  // long_note_0
    public GameObject longNoteMidPrefab;    // long_note_2
    public GameObject longNoteEndPrefab;    // long_note_1
    public GameObject Double;
    public GameObject Multi;
    public Transform pivot;

    public ReadLoadGame read;
    float speedMultiplier;
    void Start()
    {
        read = GameObject.Find("플레이!").GetComponent<ReadLoadGame>();
        speedMultiplier = read.gameData.settings.speed;
    }

    public void ShortNote()
    {
        Instantiate(Short, pivot.position, Quaternion.identity);
    }
    public void LongNote(float longTime, float bpm)
    {
        // 시간 -> 거리로 바꾸는 일인데... 거리=속력X시간
        float speed = (bpm / 60f) * speedMultiplier / 0.6f;
        float totalLength = longTime * speed / 2;
        // 길이기는한데 아마 40번째줄에서 나누니까 4가 아니라 2로 나눠야 하는 듯

        // 시작 부분 (long_note_0)
        GameObject startNote = Instantiate(longNoteStartPrefab, pivot.position, Quaternion.identity);

        // 끝 부분 (long_note_1)
        Vector3 endPosition = pivot.position + new Vector3(-totalLength, 0, 0); // 길이만큼 이동
        GameObject endNote = Instantiate(longNoteEndPrefab, endPosition, Quaternion.identity);

        // 중간 부분 (long_note_2) - 길이에 맞게 늘어나야 함
        Vector3 midPosition = (pivot.position + endPosition) / 2; // 중간 길이
        GameObject midNote = Instantiate(longNoteMidPrefab, midPosition, Quaternion.identity);
        midNote.transform.localScale = new Vector3(totalLength * 6, midNote.transform.localScale.y, 1f);
    }
    public void DoubleNote()
    {
        Instantiate(Double, pivot.position, Quaternion.identity);
    }

    public void MultiNote()
    {
        Instantiate(Multi, pivot.position, Quaternion.identity);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "note")
        {
            Destroy(other.gameObject);
        }
    }
}
