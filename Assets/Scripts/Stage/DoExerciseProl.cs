using UnityEngine;
using UnityEngine.SceneManagement;

public class DoExerciseProl : MonoBehaviour
{
    public string ReadIt;
    public void BtnExercise()
    {
        SceneManager.LoadScene(ReadIt);
    }

    public void SaveDialogue(int i)
    {
        int temp = PlayerPrefs.GetInt("DialogIndex");
        if(i >= temp)
        {
            PlayerPrefs.SetInt("DialogIndex", i);
        }
        PlayerPrefs.Save();
    }

    public void Cancle(GameObject StartUI)
    {
        StartUI.SetActive(false);
    }
}