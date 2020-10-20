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

    private void Start()
    {
        _musicImage = gameObject.GetComponent<Image>();
        _musicImage.sprite = AppState.IsMusicOn ? musicIconOn : musicIconOff;
    }

    public void ClickMusicButton()
    {
        AppState.IsMusicOn = !AppState.IsMusicOn;
        _musicImage.sprite = AppState.IsMusicOn ? musicIconOn : musicIconOff;
    }
}
