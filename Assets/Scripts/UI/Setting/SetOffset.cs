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
    public bool isInOffseting = false;
    List<float> _Offset = new List<float>();
    public AudioSource OffsetSong;
    public AudioSource OffsetSFX;
    float[] Beat = {4, 8, 12, 16};

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
            Beat[0] -= Time.deltaTime;
            Beat[1] -= Time.deltaTime;
            Beat[2] -= Time.deltaTime;
            Beat[3] -= Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.Space) && Beat[OffsetBarCount] < 1)
            {
                AddOffset();
            }
        }
        
        if(OffsetBarCount == 4)
        {
            isInOffseting = false;
            FinallSetOffset();
            OffsetSong.Stop();
            isInOffseting = false;
            OffsetBarCount++;
        }

    }

    public void SetOffsetStart()
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
        OffsetSFX.PlayOneShot(OffsetSFX.clip);
        _Offset.Add(Beat[OffsetBarCount]);
        OffsetBarCount++;
    }

    IEnumerator OffsetBarGenerate()
    {
        Instantiate(OffsetBar, new Vector3(-8f, 0, 0), Quaternion.identity);

        yield return new WaitForSeconds(4f);

        if(OffsetBarCount < 3)
        {
            StartCoroutine(OffsetBarGenerate());
        }
        else
        {
            yield break;
        }
        yield return null;
    }

    void FinallSetOffset()
    {
        float offset = 0;
        for(int i = 0; i < _Offset.Count; i++)
        {
            offset += _Offset[i];
        }
        offset /= _Offset.Count;;
        PlayerPrefs.SetFloat("PlayerOffset", offset);
        PlayerPrefs.Save();
        
        int OffsetMS = Mathf.RoundToInt(offset * 1000); 
        OffsetText.text = OffsetMS.ToString() + "ms";
    }
}
