using UnityEngine;

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
        _Offset.SetActive(false);
        Time.timeScale = 0;
    }
}
