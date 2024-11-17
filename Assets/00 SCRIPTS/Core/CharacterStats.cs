using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat Strength;
    public Stat Damage;
    public Stat MaxHealth;

    [SerializeField] protected int currentHealth;

    protected virtual void Start()
    {
        currentHealth = MaxHealth.GetValue();
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    public virtual void DoDamage(CharacterStats target)
    {
        int totalDamage = Damage.GetValue() + Strength.GetValue();
        target.TakeDamage(totalDamage);
    }

    protected virtual void Die()
    {
    }
}
