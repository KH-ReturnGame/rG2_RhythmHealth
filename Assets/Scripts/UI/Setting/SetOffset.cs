using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetOffset : MonoBehaviour
{
    public GameObject OffsetBar;
    public Transform OffsetBarTransform;
    int OffsetBarCount = 0;
    bool isInOffseting = false;
    List<float> _Offset = new List<float>();
    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isInOffseting)
        {
            AddOffset();
        }
    }

    void AddOffset()
    {
        _Offset.Add(OffsetBar.transform.position.x - OffsetBarTransform.position.x);
        FinallSetOffset();
    }

    public void SetOffetStart()
    {
        if(!isInOffseting)
        {
            isInOffseting = true;
            OffsetBarCount = 0;
            PlayerPrefs.SetFloat("PlayerOffset", 0);
            StartCoroutine(OffsetBarGenerate());
        }
    }

    IEnumerator OffsetBarGenerate()
    {
        Instantiate(OffsetBar, OffsetBarTransform.position, Quaternion.identity);
        
        yield return new WaitForSeconds(1f);
        OffsetBarCount++;
        if(OffsetBarCount < 10)
        {
            StartCoroutine(OffsetBarGenerate());
        }
        else
        {
            isInOffseting = false;
            yield break;
        }
        yield return null;
    }

    void FinallSetOffset()
    {
        float offset = 0;
        for(int i = 0; i < 10; i++)
        {
            offset += _Offset[i];   
        }
        offset /= 10;
        PlayerPrefs.SetFloat("PlayerOffset", offset);
    }
}
