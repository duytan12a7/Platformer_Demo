using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
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

    [SerializeField] private int currentHealth;

    private const int DefaultCritPower = 150;
    private const int MaxChance = 100;

    protected virtual void Start()
    {
        CriticalPower.SetDefaultValue(DefaultCritPower);
        currentHealth = MaxHealth.GetValue();
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
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
        return Random.Range(0, MaxChance) < evasionChance;
    }

    private int CalculateBaseDamage() => PhysicalDamage.GetValue() + Strength.GetValue();

    private bool IsCriticalHit()
    {
        int critChance = CriticalChance.GetValue() + Agility.GetValue();
        return Random.Range(0, MaxChance) < critChance;
    }

    private int CalculateCriticalDamage(int baseDamage)
    {
        float critMultiplier = (CriticalPower.GetValue() + Strength.GetValue()) * 0.01f;
        return Mathf.RoundToInt(baseDamage * critMultiplier);
    }

    private int ReduceDamageByArmor(CharacterStats target, int damage)
    {
        int reducedDamage = damage - target.Armor.GetValue();
        return Mathf.Clamp(reducedDamage, 0, int.MaxValue);
    }

    protected virtual void Die()
    {
        // TODO
    }
}
