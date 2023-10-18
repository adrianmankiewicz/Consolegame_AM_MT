using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject winWindow;
    private int currentCard;
    [SerializeField] List<Card> cards;

    public void AddCard()
    {
        currentCard++;

        if(currentCard == cards.Count)
        {
            FinishGame();
        }
    }

    public void FinishGame()
    {
        enemy.SetActive(false);
        winWindow.SetActive(true);

        Time.timeScale = 0f;
        AudioListener.pause = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
