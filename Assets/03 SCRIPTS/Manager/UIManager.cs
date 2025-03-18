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
    [SerializeField] private Slider sliderExp;
    [SerializeField] private TMP_Text textHP;
    [SerializeField] private TMP_Text textLevel;
    [SerializeField] private GameObject gameOverPanel;

    private void OnEnable()
    {
        GameEvent.OnHealthChanged += UpdateUI;
        GameEvent.OnExpChanged += UpdateXP;

        playerStats.OnDeath += ShowGameOver;
        playerStats.OnLevelUp += UpdateLevel;
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
    }

    private void UpdateXP()
    {
        sliderExp.maxValue = playerStats.GetXPToNextLevel();
        sliderExp.value = playerStats.GetCurrentXP();
    }

    private void UpdateLevel(int level)
    {
        textLevel.text = $"Cấp {level}";
        UpdateXP();
    }

    private void OnDisable()
    {
        GameEvent.OnHealthChanged -= UpdateUI;
        GameEvent.OnExpChanged -= UpdateXP;

        playerStats.OnDeath -= ShowGameOver;
        playerStats.OnLevelUp -= UpdateLevel;
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
