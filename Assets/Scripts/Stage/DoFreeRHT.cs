using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoFreeRHT : MonoBehaviour
{
    bool CanStart = false;
    public GameObject SelectRHTUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CanStart && Input.GetKey("f"))
        {
            SelectRHTUI.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CanStart = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CanStart = false;
        }
    }
}
