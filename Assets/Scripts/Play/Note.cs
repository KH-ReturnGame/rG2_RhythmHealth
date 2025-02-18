using System.Collections;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float BPM;
    public float speedMultiplier = 1.0f; // 뭐...배속은 나중에 만들고...
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BPM = 100;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = (BPM / 60f) * speedMultiplier; // BPM을 초당 이동 거리로 변환
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
