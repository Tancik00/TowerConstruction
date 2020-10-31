using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCubeElement : MonoBehaviour
{
   public int needToUnlock;
   public Color color;
   public GameController gameController;

   private Text _txt;
   private Image _image;
   private void Start()
   {
      _txt = GetComponentInChildren<Text>();
      _image = GetComponent<Image>();
      
      if (PlayerPrefs.GetInt("score") < needToUnlock)
      {
         _image.color = Color.black;
         _txt.text = "need to unlock: " + needToUnlock;
      }
      else
      {
         _image.color = color;
         _txt.text = "unlocked";
         gameController.possibleCubeColors.Add(color);
      }
   }
}
