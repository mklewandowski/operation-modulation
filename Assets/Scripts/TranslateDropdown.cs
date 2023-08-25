using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TranslateDropdown : MonoBehaviour
{
    public string[] EnglishText = {""};
    public string[] SpanishText = {""};

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        TMP_Dropdown dropdown = this.GetComponent<TMP_Dropdown>();
        string[] wordArray = Globals.CurrentLanguage == Globals.Language.English ? EnglishText : SpanishText;
        string[] prevWordArray = Globals.CurrentLanguage == Globals.Language.English ? SpanishText : EnglishText;
        for (int i = 0; i < wordArray.Length; i++)
        {
            dropdown.options[i].text = wordArray[i];
        }
        for (int i = 0; i < prevWordArray.Length; i++)
        {
            if (prevWordArray[i] == dropdown.captionText.text)
                dropdown.captionText.text = dropdown.options[i].text = wordArray[i];
        }
    }
}
