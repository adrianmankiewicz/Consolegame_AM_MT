using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : InteractBehaviour
{
    [SerializeField] CardManager cardManager;

    public override void OnInteract()
    {
        cardManager.AddCard();
        Destroy(gameObject);
    }
}
