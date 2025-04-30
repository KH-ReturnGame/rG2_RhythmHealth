using UnityEngine;

public class EnableOffset : MonoBehaviour
{
    public GameObject _Offset;

    public void EnableOffsetGame()
    {
        _Offset.SetActive(true);
    }

    public void DisableOffsetGame()
    {
        _Offset.SetActive(false);
    }
}
