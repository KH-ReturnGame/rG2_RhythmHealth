using UnityEngine;
using UnityEngine.UI;

public class Beats : MonoBehaviour
{
    public int HowLong = 0; // How long the beat lasts
    public InputField inputField_ID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            HowLong = int.Parse(inputField_ID.text);
        }
        catch
        {
            Debug.LogWarning("Invalid input for HowLong. Please enter a number.");
        }
    }

    public void FrameRoutine()
    {
        //HowLong
    } 
}
