using UnityEngine;
using System.Collections;

public class SpinDisc : MonoBehaviour
{
    [Header("회전 설정")]
    [Tooltip("초당 회전 속도 (도)")]
    public float rotationSpeed = 90f;

    [Header("펄스(크기 변화) 설정")]
    [Tooltip("크기 변화 진폭 (원래 크기에 대한 비율, 예: 0.2는 20% 증가)")]
    public float pulseAmplitude = 0.05f;
    [Tooltip("박자 (초당 반복 횟수)")]
    public float pulseFrequency = 1f;
    [Tooltip("펄스 애니메이션 지속 시간 (초, 박자 기간보다 작아야 함)")]
    public float pulseDuration = 0.3f;

    private RectTransform rectTransform;
    private Vector3 originalScale;
    private float beatInterval;

    void Start()
    {
        // RectTransform 컴포넌트를 가져오고 원래 크기를 저장합니다.
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        beatInterval = 1f / pulseFrequency;
        
        // 펄스 애니메이션 코루틴 시작
        StartCoroutine(PulseRoutine());
    }

    void Update()
    {
        // 계속 회전 효과 적용
        rectTransform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    // 펄스 애니메이션을 박자에 맞춰 실행하는 코루틴
    IEnumerator PulseRoutine()
    {
        while (true)
        {
            // (박자 주기 - 펄스 애니메이션 시간) 만큼 대기한 후에 펄스 실행
            yield return new WaitForSeconds(beatInterval - pulseDuration);
            yield return PulseAnimation();
        }
    }

    // 한 번의 펄스 애니메이션 (크기가 커졌다 작아짐)
    IEnumerator PulseAnimation()
    {
        float elapsed = 0f;
        while (elapsed < pulseDuration)
        {
            float t = elapsed / pulseDuration; // 0~1 범위
            // 사인 함수를 이용하여 부드럽게 커졌다 작아지게 함
            // t=0과 t=1에서는 원래 크기, t=0.5에서 최대 크기 (원래 * (1 + pulseAmplitude))
            float scaleFactor = 1 + pulseAmplitude * Mathf.Sin(Mathf.PI * t);
            rectTransform.localScale = originalScale * scaleFactor;
            elapsed += Time.deltaTime;
            yield return null;
        }
        // 펄스 종료 후 정확히 원래 크기로 복원
        rectTransform.localScale = originalScale;
    }
}
