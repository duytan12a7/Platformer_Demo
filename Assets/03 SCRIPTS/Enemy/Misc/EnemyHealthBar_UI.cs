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

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        enemyStats = enemy.GetComponentInChildren<EnemyStats>();
        rectTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        enemy.OnFlipped += Flip;
        GameEvent.OnHealthChanged += UpdateHealthUI;
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = enemyStats.GetMaxHealthValue();
        slider.value = enemyStats.CurrentHealth;
    }

    private void Flip() => rectTransform.Rotate(0f, 180f, 0f);

    private void OnDisable()
    {
        enemy.OnFlipped -= Flip;
        GameEvent.OnHealthChanged -= UpdateHealthUI;
    }
}
