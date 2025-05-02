using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetOffset : MonoBehaviour
{
    public GameObject OffsetBar;
    int OffsetBarCount = 0;
    bool isInOffseting = false;
    List<float> _Offset = new List<float>();
    public AudioSource OffsetSong;
    float Beat = 4f;
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
        _Offset.Add();
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
        Instantiate(OffsetBar, new Vector3(-8.85f, 0, 0), Quaternion.identity);
        
        yield return new WaitForSeconds(4f);
        OffsetBarCount++;
        if(OffsetBarCount < 4)
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
        for(int i = 0; i < 4; i++)
        {
            offset += _Offset[i];   
        }
        offset /= 4;
        PlayerPrefs.SetFloat("PlayerOffset", offset);
    }
}
