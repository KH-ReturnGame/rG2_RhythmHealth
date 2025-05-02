using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableOffset : MonoBehaviour // UI 껐다 켰다 스크립트
{
    public GameObject _Offset;

    public void EnableOffsetGame()
    {
        _Offset.SetActive(true);
        Time.timeScale = 1;
    }

    public void DisableOffsetGame()
    {
        SetOffset setOffset = _Offset.GetComponent<SetOffset>();
        if(!setOffset.isInOffseting)
        {
            _Offset.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
