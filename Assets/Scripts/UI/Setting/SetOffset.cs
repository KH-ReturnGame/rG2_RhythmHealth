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
    public AudioSource OffsetSFX;
    float Beat = 4f;

    public TextMeshProUGUI OffsetText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInOffseting)
        {
            Beat -= Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.Space) && Beat < 1)
            {
                AddOffset();
            }
        }
        
        if(OffsetBarCount == 4)
        {
            FinallSetOffset();
            OffsetSong.Stop();
            isInOffseting = false;
            OffsetBarCount++;
        }

    }

    public void SetOffetStart()
    {
        if(!isInOffseting)
        {
            OffsetSong.time = 0;
            OffsetSong.Play();

            isInOffseting = true;
            OffsetBarCount = 0;

            PlayerPrefs.SetFloat("PlayerOffset", 0);
            StartCoroutine(OffsetBarGenerate());
        }
    }

    void AddOffset()
    {
        OffsetSFX.Play();
        _Offset.Add(Beat);
    }

    IEnumerator OffsetBarGenerate()
    {
        Instantiate(OffsetBar, new Vector3(-8f, 0, 0), Quaternion.identity);
        Beat = 4f;
        
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
        
        int OffsetMS = Mathf.RoundToInt(offset * 1000); 
        OffsetText.text = OffsetMS.ToString() + "ms";
    }
}
