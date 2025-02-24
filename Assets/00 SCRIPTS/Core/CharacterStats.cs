using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    [Header("Primary Attributes")]
    public Stat Strength; // Increases physical damage and critical power.
    public Stat Agility; // Increases evasion and critical chance.
    public Stat Vitality; // Increases health by a fixed amount per point.

    [Header("Offensive Stats")]
    public Stat PhysicalDamage;
    public Stat CriticalChance;
    public Stat CriticalPower;

    [Header("Defensive Stats")]
    public Stat MaxHealth;
    public Stat Armor;
    public Stat Evasion;


    [Header("Current Stats")]
    public int CurrentHealth;

    public float CurrentMovementSpeed;

    private const int defaultCritPower = 150;
    private const int maxChance = 100;
    private const float multiplier = 0.01f;

    protected virtual void Start()
    {
        CriticalPower.SetDefaultValue(defaultCritPower);
        CurrentHealth = GetMaxHealthValue();
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        GameEvent.CallOnHealthChanged();
        if (CurrentHealth <= 0)
            Die();
    }

    public virtual void PerformAttack(CharacterStats target)
    {
        if (IsAttackEvaded(target)) return;

        int damage = CalculateBaseDamage();

        if (IsCriticalHit())
            damage = CalculateCriticalDamage(damage);

        damage = ReduceDamageByArmor(target, damage);

        target.TakeDamage(damage);
    }

    private bool IsAttackEvaded(CharacterStats target)
    {
        int evasionChance = target.Evasion.GetValue() + target.Agility.GetValue();
        return Random.Range(0, maxChance) < evasionChance;
    }

    private int CalculateBaseDamage() => PhysicalDamage.GetValue() + Strength.GetValue();

    private bool IsCriticalHit()
    {
        int critChance = CriticalChance.GetValue() + Agility.GetValue();
        return Random.Range(0, maxChance) < critChance;
    }

    private int CalculateCriticalDamage(int baseDamage)
    {
        float critMultiplier = (CriticalPower.GetValue() + Strength.GetValue()) * multiplier;
        return Mathf.RoundToInt(baseDamage * critMultiplier);
    }

    private int ReduceDamageByArmor(CharacterStats target, int damage)
    {
        int reducedDamage = damage - target.Armor.GetValue();
        return Mathf.Clamp(reducedDamage, 0, int.MaxValue);
    }

    public int GetMaxHealthValue() => MaxHealth.GetValue() + Vitality.GetValue() * 5;

    public void Reset()
    {
        CurrentHealth = GetMaxHealthValue(); 
        CriticalPower.SetDefaultValue(defaultCritPower);
        GameEvent.CallOnHealthChanged();
    }

    public virtual void UpdateMovementSpeed(float newSpeed)
    {
        CurrentMovementSpeed = newSpeed;
    }

    protected abstract void Die();
}
