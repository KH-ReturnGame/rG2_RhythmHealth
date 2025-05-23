using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    public GameObject[] _AbleSettings;
    [SerializeField] private int _SettingIdx;
    int _ScreenWidth, _ScreenHeight;
    bool _FullScreenOn;
    // Start is called before the first frame update
    void Start()
    {
        _SettingIdx = 0;
        _ScreenWidth = 1920;
        _ScreenHeight = 1080;
        _FullScreenOn = true;
        Screen.SetResolution(_ScreenWidth, _ScreenHeight, _FullScreenOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextSetting(int _next)
    {
        _AbleSettings[_SettingIdx].SetActive(false);
        if(_SettingIdx + _next > _AbleSettings.Length - 1)
        {
            _SettingIdx = 0;
        }
        else if(_SettingIdx + _next < 0)
        {
            _SettingIdx = _AbleSettings.Length - 1;
        }
        else
        {
            _SettingIdx += _next;
        }
        Debug.Log(_SettingIdx);
        _AbleSettings[_SettingIdx].SetActive(true);
    }

    public void SetScreen()
    {
        switch (_SettingIdx)
        {
            case 0:
                _ScreenWidth = 1920;
                _ScreenHeight = 1080;
                break;
            case 1:
                _ScreenWidth = 1280;
                _ScreenHeight = 720;
                break;
            case 2:
                _ScreenWidth = 960;
                _ScreenHeight = 540;
                break;
            case 3:
                _ScreenWidth = 720;
                _ScreenHeight = 480;
                break;
            case 4:
                _ScreenWidth = 640;
                _ScreenHeight = 360;
                break;
            default:
                _ScreenWidth = 1920;
                _ScreenHeight = 1080;
                break;
        }
        GameObject _Canvas = GameObject.Find("Canvas");
        CanvasScaler canvasScaler = _Canvas.GetComponent<CanvasScaler>();
        canvasScaler.matchWidthOrHeight = 0.5f;
        
        Screen.SetResolution(_ScreenWidth, _ScreenHeight, _FullScreenOn);

        PlayerPrefs.SetInt("ScreenWidth", _ScreenWidth);// 값 저장
        PlayerPrefs.SetInt("ScreenHeight", _ScreenHeight);
    }
    public void SetFullScreen()
    {
        _FullScreenOn = (_SettingIdx == 0); //_SettingIdx이 0이면 true, 아니면 false, 코드 정상화 성공
        Screen.fullScreen = _FullScreenOn;
        PlayerPrefs.SetInt("IntFullScreen", _SettingIdx); // 이걸 왜 저장하는지 모르겠지만 일단 저장22
    }
}