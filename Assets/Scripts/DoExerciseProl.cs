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
        PlayerPrefs.SetInt("DialogIndex", i);
        PlayerPrefs.Save();
    }
}