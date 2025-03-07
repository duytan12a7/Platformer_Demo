using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar_UI : MonoBehaviour
{
    private Player player;
    private PlayerStats playerStats;
    private RectTransform rectTransform;
    private Slider slider;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        playerStats = player.GetComponentInChildren<PlayerStats>();
        rectTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        player.OnFlipped += Flip;
        GameEvent.OnHealthChanged += UpdateHealthUI;
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.CurrentHealth;
    }

    private void Flip() => rectTransform.Rotate(0f, 180f, 0f);

    private void OnDisable()
    {
        player.OnFlipped -= Flip;
        GameEvent.OnHealthChanged -= UpdateHealthUI;
    }
}
