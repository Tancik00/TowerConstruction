using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Sprite musicIconOn;
    public Sprite musicIconOff;

    private Image _musicImage;
    
    public static bool IsMusicOn
    {
        get => PlayerPrefs.GetInt("musicOn",1)==1;
        set => PlayerPrefs.SetInt("musicOn", value?1:0);
    } 

    private void Start()
    {
        _musicImage = gameObject.GetComponent<Image>();
        _musicImage.sprite = IsMusicOn ? musicIconOn : musicIconOff;
    }

    public void ClickMusicButton()
    {
        IsMusicOn = !IsMusicOn;
        _musicImage.sprite = IsMusicOn ? musicIconOn : musicIconOff;
    }
}
