using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar_UI : MonoBehaviour
{
    private Enemy enemy;
    private EnemyStats enemyStats;
    private RectTransform rectTransform;
    private Slider slider;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        enemyStats = enemy.GetComponentInChildren<EnemyStats>();

        rectTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();

        enemy.OnFlipped += Flip;
        EventHandler.OnHealthChanged += UpdateHealthUI;
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = enemyStats.GetMaxHealthValue();
        slider.value = enemyStats.CurrentHealth;
    }

    private void Flip() => rectTransform.Rotate(0, 180, 0);

    private void OnDisable()
    {
        enemy.OnFlipped -= Flip;
        EventHandler.OnHealthChanged -= UpdateHealthUI;
    }
}
