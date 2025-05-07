using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Warn : MonoBehaviour
{
    public GameObject warningPanel; // Reference to the warning panel GameObject
    public Transform canvasTransform;
    public TextMeshProUGUI warningText; // Reference to the warning text GameObject

    public void make_Warn(string message)
    {
        // Set the warning message
        warningText.text = message;
        
        // Show the warning panel
        Instantiate(warningPanel, canvasTransform);
    }

    public void BackToStage()
    {
        SceneManager.LoadScene("Stage");
    }
}