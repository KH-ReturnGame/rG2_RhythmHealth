using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour
{
    public GameObject _Settings;
    GameObject _Canvas;
    // Start is called before the first frame update
    void Start()
    {
        _Canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableMenu()
    {
        this.gameObject.SetActive(false);
        if(_Settings != null)
        {
            _Settings.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void SettingGame()
    {
        _Settings.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
