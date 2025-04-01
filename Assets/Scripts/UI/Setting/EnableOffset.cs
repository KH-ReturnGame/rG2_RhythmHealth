using UnityEngine;

public class EnableOffset : MonoBehaviour
{
    public GameObject _Offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableOffsetGame()
    {
        _Offset.SetActive(true);
        Debug.Log("Enable Offset Game");
    }

    public void DisableOffsetGame()
    {
        _Offset.SetActive(false);
    }
}
