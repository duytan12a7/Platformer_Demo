using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Entity entity;
    private CharacterStats characterStats;
    private RectTransform rectTransform;
    private Slider slider;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
        characterStats = entity.GetComponentInChildren<CharacterStats>();
        rectTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
    }


    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        entity.OnFlipped += Flip;
        GameEvent.OnHealthChanged += UpdateHealthUI;
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = characterStats.GetMaxHealthValue();
        slider.value = characterStats.CurrentHealth;
    }

    private void Flip() => rectTransform.Rotate(0f, 180f, 0f);

    private void OnDisable()
    {
        entity.OnFlipped -= Flip;
        GameEvent.OnHealthChanged -= UpdateHealthUI;
    }
}
