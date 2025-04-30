using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetOffset : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOffetStart()
    {
        List<float> _Offset = new List<float>();
        float offset = 0;
        for(int i = 0; i < 10; i++)
        {
            _Offset.Add(0);
        }
        for(int i = 0; i < 10; i++)
        {
            offset += _Offset[i];   
        }
        offset /= 10;
    }
}
