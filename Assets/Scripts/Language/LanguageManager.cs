using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;

    public Language CurrentLanguage { private set; get; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SetLanguage(Language.English);
    }

    public void SetLanguageIndex(int index)
    {
        switch(index)
        {
            case 0:
                SetLanguage(Language.Polish);
                break;
            case 1:
                SetLanguage(Language.English);
                break;
            case 2:
                SetLanguage(Language.Spanish);
                break;
        }
    }
    public void SetLanguage(Language lang)
    {
        CurrentLanguage = lang;

        SwitchLanguage[] allSL = FindObjectsOfType<SwitchLanguage>();
        for (int i = 0; i < allSL.Length; i++)
        {
            allSL[i].OnRefresh();
        }
    }
}

public enum Language
{
    Polish,
    English,
    Spanish
}
