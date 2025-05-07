using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GymEquip : MonoBehaviour
{
    public bool CanStart;
    public GameObject Lock;
    public GameObject Finteract;
    public GameObject StartPT_UI;
    public GameObject StartPT_Btn;
    private bool CanPT;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Lock.SetActive(!CanStart);
    }

    // Update is called once per frame
    void Update()
    {
        if(CanPT && Input.GetKey("f"))
        {
            var parent = StartPT_UI.transform;
            for (int i = 0; i < parent.childCount; i++)
            {
                parent.GetChild(i).gameObject.SetActive(false);
            }
            StartPT_UI.SetActive(true);
            StartPT_Btn.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && CanStart)
        {
            Lock.SetActive(!CanStart);
            Finteract.SetActive(true);
            CanPT = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && CanStart)
        {
            Lock.SetActive(!CanStart);
            Finteract.SetActive(false);
            CanPT = false;
        }
    }
}
