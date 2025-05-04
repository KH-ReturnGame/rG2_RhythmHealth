using UnityEngine;
using UnityEngine.SceneManagement;

public class DoExerciseProl : MonoBehaviour
{
    public string ReadIt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnExercise()
    {
        SceneManager.LoadScene(ReadIt);
    }

    public void SaveDialogue(int i)
    {
        PlayerPrefs.SetInt("DialogIndex", i);
        PlayerPrefs.Save();
    }
}