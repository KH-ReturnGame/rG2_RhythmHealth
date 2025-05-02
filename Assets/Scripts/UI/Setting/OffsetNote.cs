using System.Collections;
using UnityEngine;

public class OffsetNote : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float speed = (72 / 60f) / 0.6f; // BPM을 초당 이동 거리로 변환
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}