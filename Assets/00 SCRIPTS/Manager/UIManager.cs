using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider sliderHP;

    private void Awake()
    {
        sliderHP = GetComponentInChildren<Slider>();
    }

    private void OnEnable()
    {
        EventHandler.OnHealthChanged += UpdateUI;
    }

    private void UpdateUI()
    {
        sliderHP.maxValue = playerStats.GetMaxHealthValue();
        sliderHP.value = playerStats.CurrentHealth;
    }

    private void OnDisable()
    {
        EventHandler.OnHealthChanged -= UpdateUI;
    }
}
