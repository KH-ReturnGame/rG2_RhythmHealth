using UnityEngine;
using UnityEngine.UI;

public class Beats : MonoBehaviour
{
    public int HowLong = 0; // How long the beat lasts
    public InputField inputField_ID;
    public InputField BPM_ID;
    public GameObject beatPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            HowLong = int.Parse(inputField_ID.text);
        }
        catch
        {
            Debug.LogWarning("Invalid input for HowLong. Please enter a number.");
        }
    }

    public void FrameRoutine()
    {
        if (!string.IsNullOrEmpty(BPM_ID.text) && !string.IsNullOrEmpty(inputField_ID.text))
        {
            float frameCount = HowLong * int.Parse(BPM_ID.text) / 60f;
            Transform content = GetComponent<ScrollRect>().content;
    
            // 기존 자식 오브젝트 제거
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
    
            // 새 비트 오브젝트 생성
            for (int i = 0; i < frameCount; i++)
            {
                GameObject beat = Instantiate(beatPrefab, content);
                beat.name = "Beat_" + i;
            }
        }
    } 
}
