using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    [SerializeField] GameObject enemy;
    [SerializeField] GameObject winWindow;
    [SerializeField] GameObject loseWindow;
    private int currentCard;
    [SerializeField] List<Card> cards;
    [SerializeField] TMPro.TMP_Text countPapers;

    private void Awake()
    {
        Instance = this;
        RefreshPaperCount();
    }

    public void AddCard()
    {
        currentCard++;

        if(currentCard == cards.Count)
        {
            FinishGame();
        }

        RefreshPaperCount();
    }

    public void FinishGame()
    {
        enemy.SetActive(false);
        winWindow.SetActive(true);

        Time.timeScale = 0f;
        AudioListener.pause = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoseGame()
    {
        enemy.SetActive(false);
        loseWindow.SetActive(true);

        Time.timeScale = 0f;
        AudioListener.pause = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void RefreshPaperCount()
    {
        TextWSL countText = new TextWSL("Kartki ", "Papers ", "tarjetas ");

        countPapers.text = countText.GetText + currentCard.ToString() + "/" + cards.Count;
    }
}
