using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppState 
{
    public static bool IsMusicOn
    {
        get => PlayerPrefs.GetInt("musicOn",1)==1;
        set => PlayerPrefs.SetInt("musicOn", value?1:0);
    } 
}
