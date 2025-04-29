using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterGame : MonoBehaviour
{
    public TextAsset[] jsonList;
    public Image icon;
    public TextMeshProUGUI SongDesc;
    public int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        icon = GetComponent<Image>();
        foreach (string directoryPath in Directory.EnumerateDirectories("C:/RHRoutines"))
        {
            string[] jsonFiles = Directory.GetFiles(directoryPath, "*.json", SearchOption.AllDirectories);
            foreach (string file in jsonFiles)
            {
                TextAsset textAsset = new TextAsset(File.ReadAllText(file));
                jsonList[index] = textAsset;
                index++;
            }
        }
        LoadData();
    }

    public void LoadData()
    {
        // icon.sprite = ;
        SongDesc.text = jsonList[index].text;   
    }

    public void RightData()
    {
        index++;
        if (index >= jsonList.Length)
        {
            index = 0;
        }
        LoadData();
    }

    public void LeftData()
    {
        index--;
        if (index < 0)
        {
            index = jsonList.Length - 1;
        }
    }
}
