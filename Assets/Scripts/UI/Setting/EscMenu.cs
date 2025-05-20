using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour
{
    public GameObject _Settings;
    public GameObject MusicPlayerObj;
    AudioSource MusicPlayer;

    void OnEnable()
    {
        Time.timeScale = 0;
        if(MusicPlayerObj != null)
        {
            MusicPlayer = MusicPlayerObj.GetComponent<AudioSource>();
            MusicPlayer.Pause();
        }
    }

    void OnDisable()
    {
        Time.timeScale = 1;
        if(MusicPlayerObj != null)
        {
            MusicPlayer.UnPause();
        }
    }

    public void DisableMenu()
    {
        this.gameObject.SetActive(false);
        if (_Settings != null)
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

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
