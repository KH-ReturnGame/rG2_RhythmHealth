using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public GameObject SetObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exercise()
    {
        PlayerPrefs.SetInt("DialogIndex", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Stage");
    }

    public void Settings()
    {
        SetObj.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
