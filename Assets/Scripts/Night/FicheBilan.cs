using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FicheBilan : MonoBehaviour
{
    private Image _image;
    public List<Sprite> fiches;
    
    private void Start()
    {
        _image = GetComponent<Image>();

        _image.sprite = fiches[GameData.NumberDays - 1];
    }
}
