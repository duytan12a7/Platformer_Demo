using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_UserInfo : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider sliderHP;
    [SerializeField] private Slider sliderExp;
    [SerializeField] private TMP_Text textHP;
    [SerializeField] private TMP_Text textLevel;

    private void OnEnable()
    {
        GameEvent.OnHealthChanged += UpdateUI;
        GameEvent.OnExpChanged += UpdateXP;

        playerStats.OnLevelUp += UpdateLevel;
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

        playerStats.OnLevelUp -= UpdateLevel;
    }
}
