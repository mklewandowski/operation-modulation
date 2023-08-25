using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    public enum BinaryImageSquareStates
    {
        On,
        Off,
        Unknown,
    }

    public static bool AudioOn = true;

    public enum Language {
        English,
        Spanish,
    };
    public static Language CurrentLanguage = Language.English;

    public const string LanguageStorageKey = "Language";

    public static void SaveIntToPlayerPrefs(string key, int val)
    {
        PlayerPrefs.SetInt(key, val);
    }
    public static int LoadIntFromPlayerPrefs(string key, int defaultVal = 0)
    {
        int val = PlayerPrefs.GetInt(key, defaultVal);
        return val;
    }

    public static void LoadUserSettings()
    {
        int lang = Globals.LoadIntFromPlayerPrefs(Globals.LanguageStorageKey);
        Globals.CurrentLanguage = (Globals.Language)lang;
    }
}
