using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperPupSystems.Helper;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Health health;
    
    public void Start()
    {
        slider.maxValue = health.maxHealth;
        SetHealth();
    }

    public void SetHealth()
    {
        slider.value = health.currentHealth;
    }
}
