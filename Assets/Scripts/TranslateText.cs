using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TranslateText : MonoBehaviour
{
    public string[] TranslatedText = {"",""};
    public bool AdjustFont = false;
    public int FontSize = 0;
    public int LineSpacing = 0;

    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        int lang = (int)(Globals.CurrentLanguage);
        TextMeshProUGUI gameObjectText = this.GetComponent<TextMeshProUGUI>();
        if (Globals.CurrentLanguage == Globals.Language.Spanish && AdjustFont)
        {
            if (FontSize > 0) gameObjectText.fontSize = FontSize;
            if (LineSpacing != 0) gameObjectText.lineSpacing = LineSpacing;
        }
        gameObjectText.text = TranslatedText[lang].Replace("\\n", "\n");
    }
}
