using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFloatAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] frames;      // 7개 스프라이트
    [SerializeField] private float interval = 0.25f; // 프레임 전환 간격
    [SerializeField] private float amplitude = 0.1f; // 위아래 이동 폭

    private SpriteRenderer _sr;
    private Vector3 _startPos;
    private int _idx = 0;
    private float _timer = 0f;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _startPos = transform.localPosition;
    }

    void Update()
    {
        // 1) 프레임 전환
        _timer += Time.deltaTime;
        if (_timer >= interval)
        {
            _timer -= interval;
            _idx = (_idx + 1) % frames.Length;
            _sr.sprite = frames[_idx];
        }

        // 2) 둥둥 떠오르기
        float y = Mathf.Sin(Time.time * (2f * Mathf.PI / 2)) * amplitude;
        transform.localPosition = _startPos + Vector3.up * y;
    }
}