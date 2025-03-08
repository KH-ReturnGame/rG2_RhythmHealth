using System.Collections;
using UnityEngine;

public class Note : MonoBehaviour
{
    ReadLoadGame read;
    public float BPM_note;
    public float speedMultiplier;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        read = GameObject.Find("판?").GetComponent<ReadLoadGame>();
        BPM_note = read.gameData.settings.bpm;
        speedMultiplier = read.gameData.settings.speed;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = (BPM_note / 60f) * speedMultiplier; // BPM을 초당 이동 거리로 변환
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
