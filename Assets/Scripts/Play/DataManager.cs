using UnityEngine;
using System.Collections;
public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public TextAsset jsonData;
    public AudioClip audioClip;
    public bool isPre;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
