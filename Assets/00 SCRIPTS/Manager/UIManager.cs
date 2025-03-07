using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider sliderHP;
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
        DefaultPanel();
    }

    private void DefaultPanel()
    {
        gameOverPanel.SetActive(false);
    }

    private void UpdateUI()
    {
        sliderHP.maxValue = playerStats.GetMaxHealthValue();
        sliderHP.value = playerStats.CurrentHealth;
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
