using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public float Health;
    [SerializeField] UnityEvent onGetDamage;
    [SerializeField] Slider healthSlider;

    private void Awake()
    {
        RefreshSlider(); 
    }

    public void GetDamage(float damage)
    {
        Health -= damage;
        onGetDamage?.Invoke();

        RefreshSlider();
        if (Health <= 0)
        {
            CardManager.Instance.LoseGame();
        }
    }

    private void RefreshSlider()
    {
        healthSlider.value = Health;
    }
}
