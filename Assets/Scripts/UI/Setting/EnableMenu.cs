using UnityEngine;

public class EnableMenu : MonoBehaviour
{
    public GameObject settingObject; // 설정할 오브젝트를 드래그해서 연결

    void Update()
    {
        // ESC 키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingObject != null)
            {
                // 설정 오브젝트 활성화
                settingObject.SetActive(true);
            }
        }
    }
}
