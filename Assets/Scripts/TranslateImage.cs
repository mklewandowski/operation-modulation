using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslateImage : MonoBehaviour
{
    public Sprite[] TranslatedImage = new Sprite[2];

    void Start()
    {
        UpdateImage();
    }
    public void UpdateImage()
    {
        int lang = (int)(Globals.CurrentLanguage);
        this.GetComponent<Image>().sprite = TranslatedImage[lang];
    }
}
