using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour
{
    public GameObject _Settings;

    void OnEnable()
    {
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
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
