using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider sliderHP;
    [SerializeField] private TMP_Text textHP;
    [SerializeField] private TMP_Text textLevel;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        sliderHP = GetComponentInChildren<Slider>();
    }

    private void OnEnable()
    {
        GameEvent.OnHealthChanged += UpdateUI;
        playerStats.OnDeath += ShowGameOver;
    }

    private void Start()
    {
        UpdateUI();
        DefaultPanel();
    }

    private void DefaultPanel()
    {
        gameOverPanel.SetActive(false);
    }

    private void UpdateUI()
    {
        int currentHealth = playerStats.CurrentHealth;
        int maxHealth = playerStats.GetMaxHealthValue();

        sliderHP.maxValue = maxHealth;
        sliderHP.value = currentHealth;
        textHP.text = $"{currentHealth}/{maxHealth}";
        textLevel.text = $"Cấp 1";
    }

    private void OnDisable()
    {
        GameEvent.OnHealthChanged -= UpdateUI;
        playerStats.OnDeath -= ShowGameOver;
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
