using UnityEngine;

public class SkipDialog : MonoBehaviour
{
    bool skip = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Skip(GameObject obj)
    {
        // Skip the dialog
        obj.SetActive(false);
        skip = true;
        
    }

    public bool SkipBool()
    {
        // Skip the dialog
        return skip;
    }
}
