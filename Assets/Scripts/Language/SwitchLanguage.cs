using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLanguage : MonoBehaviour
{
    private TMPro.TMP_Text text;

    [SerializeField] string pl;
    [SerializeField] string eng;
    [SerializeField] string span;

    private void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }
    private void OnEnable()
    {
        OnRefresh();
    }

    public void OnRefresh()
    {
        if (LanguageManager.Instance == null)
            return;
        Language currentLang = LanguageManager.Instance.CurrentLanguage;
        switch (currentLang)
        {
            case Language.Polish:
                text.text = pl;
                break;
            case Language.English:
                text.text = eng;
                break;
            case Language.Spanish:
                text.text = span;
                break;
        }
    }
}

public class TextWSL
{
    public TextWSL(string pl, string eng, string span)
    {
        this.pl = pl;
        this.eng = eng;
        this.span = span;
    }

    private string pl;
    private string eng;
    private string span;

    public string GetText
    {
        get
        {
            switch (LanguageManager.Instance.CurrentLanguage)
            {
                case Language.Polish:
                    return pl;
                case Language.English:
                    return eng;
                case Language.Spanish:
                    return span;
            }
            return "";
        }
    }
}
