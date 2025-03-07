using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    private EntityFX fx;

    [Header("Primary Attributes")]
    public Stat Strength; // Increases physical damage and critical power.
    public Stat Agility; // Increases evasion and critical chance.
    public Stat Vitality; // Increases health by a fixed amount per point.
    public Stat Intelligence;

    [Header("Offensive Stats")]
    public Stat PhysicalDamage;
    public Stat CriticalChance;
    public Stat CriticalPower;
    public void AddModifyPhysicalDamage(int amount) => PhysicalDamage.AddModifier(amount);
    public void AddModifyCriticalChance(int amount) => CriticalChance.AddModifier(amount);

    [Header("Defensive Stats")]
    public Stat MaxHealth;
    public Stat Armor;
    public Stat MagicResistance;
    public Stat Evasion;
    public void AddModifyArmor(int amount) => Armor.AddModifier(amount);

    [Header(" Magic Stats")]
    public Stat FireDamage;
    public Stat IceDamage;
    public Stat LightningDamage;

    public bool IsIgnited; // Does damage over time
    public bool IsChilled; // Reduce armor by 20%
    public bool IsShocked; // Reduce accuracy by 20%


    [SerializeField] private float ailmentsDuration = 4;
    private float ignitedTimer;
    private float chilledTimer;
    private float shockedTimer;
    private float damageCooldown = .3f;
    private float igniteDamageTimer;
    private int igniteDamage;
    [SerializeField] private GameObject shockStrikePrefab;
    private float shockDamageTimer;
    private int shockDamage;
    private float chillDamageTimer;
    private int chillDamage;

    [Header(" Ailments Strike Stat ")]
    public bool HasFireStrike;
    public bool HasIceStrike;
    public bool HasElectricStrike;

    [Header("Current Stats")]
    public int CurrentHealth;

    public float CurrentMovementSpeed;

    private const int defaultCritPower = 150;
    private const int maxChance = 100;
    private const float multiplier = 0.01f;

    public bool IsDead { get; private set; }

    protected virtual void Start()
    {
        CriticalPower.SetDefaultValue(defaultCritPower);
        CurrentHealth = GetMaxHealthValue();
        fx = GetComponentInParent<EntityFX>();
    }

    protected virtual void Update()
    {
        ignitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;

        igniteDamageTimer -= Time.deltaTime;
        chillDamageTimer -= Time.deltaTime;
        shockDamageTimer -= Time.deltaTime;

        if (ignitedTimer < 0)
            IsIgnited = false;

        if (chilledTimer < 0)
            IsChilled = false;

        if (shockedTimer < 0)
            IsShocked = false;

        if (IsIgnited)
            ApplyIgniteDamage();

        if (IsChilled)
            ApplyChillDamage();

        if (IsShocked)
            ApplyShockDamage();
    }

    #region Damage

    public virtual void TakeDamage(int damage, Transform attacker)
    {
        DecreaseHealthBy(damage);
        if (CurrentHealth <= 0)
            Die();
    }

    public virtual void DoDamage(CharacterStats target, Transform attacker)
    {
        if (IsAttackEvaded(target)) return;

        int damage = CalculateBaseDamage();

        if (IsCriticalHit())
            damage = CalculateCriticalDamage(damage);

        damage = CheckTargetArmor(target, damage);

        target.TakeDamage(damage, attacker);

        if (HasFireStrike)
        {
            target.ApplyAilments(true, false, false);
            target.SetupIgniteDamage(Mathf.RoundToInt(damage * 0.2f));
        }
        else if (HasIceStrike)
        {
            target.ApplyAilments(false, true, false);
            target.SetupChillDamage(Mathf.RoundToInt(damage * 0.1f));
        }
        else if (HasElectricStrike)
        {
            target.ApplyAilments(false, false, true);
            target.SetupShockDamage(Mathf.RoundToInt(damage * 0.1f));
        }
    }

    #endregion

    #region Magical damage and Ailments

    private void ApplyAilments(bool isIgnite, bool isChill, bool isShock)
    {
        bool canApplyIgnite = !IsIgnited && !IsChilled && !IsShocked;
        bool canApplyChill = !IsIgnited && !IsChilled && !IsShocked;
        bool canApplyShock = !IsIgnited && !IsChilled;

        if (isIgnite && canApplyIgnite)
        {
            IsIgnited = isIgnite;
            ignitedTimer = ailmentsDuration;

            fx.IgniteFxFor(ailmentsDuration);
        }

        if (isChill && canApplyChill)
        {
            IsChilled = isChill;
            chilledTimer = ailmentsDuration;

            float slowPercentage = .2f;

            GetComponentInParent<Entity>().SlowEntityBy(slowPercentage, ailmentsDuration);
            fx.ChillFxFor(ailmentsDuration);
        }

        if (isShock && canApplyShock)
        {
            IsShocked = isShock;
            shockedTimer = ailmentsDuration;

            fx.ShockFxFor(ailmentsDuration);
        }
    }

    private void ApplyIgniteDamage()
    {
        if (igniteDamageTimer < 0)
        {
            DecreaseHealthBy(igniteDamage);

            if (CurrentHealth < 0 && !IsDead)
                Die();

            igniteDamageTimer = damageCooldown;
        }
    }

    private void ApplyChillDamage()
    {
        if (chillDamageTimer < 0)
        {
            DecreaseHealthBy(chillDamage);

            if (CurrentHealth < 0 && !IsDead)
                Die();

            chillDamageTimer = damageCooldown;
        }
    }

    private void ApplyShockDamage()
    {
        if (shockDamageTimer < 0)
        {
            DecreaseHealthBy(shockDamage);

            if (CurrentHealth < 0 && !IsDead)
                Die();

            shockDamageTimer = damageCooldown;
        }
    }

    public void SetupIgniteDamage(int damage) => igniteDamage = damage;
    public void SetupChillDamage(int damage) => chillDamage = damage;
    public void SetupShockDamage(int damage) => shockDamage = damage;

    #endregion

    #region Stat Calculations

    private bool IsAttackEvaded(CharacterStats target)
    {
        int evasionChance = target.Evasion.GetValue() + target.Agility.GetValue();
        if (IsChilled)
            evasionChance += 20;
        return Random.Range(0, maxChance) < evasionChance;
    }
    private int CalculateBaseDamage() => PhysicalDamage.GetValue() + Strength.GetValue();

    private bool IsCriticalHit()
    {
        int criticalChance = CriticalChance.GetValue() + Agility.GetValue();
        return Random.Range(0, maxChance) < criticalChance;
    }

    private int CalculateCriticalDamage(int baseDamage)
    {
        float critMultiplier = (CriticalPower.GetValue() + Strength.GetValue()) * multiplier;
        return Mathf.RoundToInt(baseDamage * critMultiplier);
    }

    private int CheckTargetArmor(CharacterStats target, int damage)
    {
        if (IsChilled)
            damage -= Mathf.RoundToInt(target.Armor.GetValue() * 0.8f);
        else
            damage -= target.Armor.GetValue();

        return Mathf.Clamp(damage, 0, int.MaxValue);
    }

    public int GetMaxHealthValue() => MaxHealth.GetValue() + Vitality.GetValue() * 5;

    #endregion

    #region Others Function

    public void Reset()
    {
        CurrentHealth = GetMaxHealthValue();
        CriticalPower.SetDefaultValue(defaultCritPower);
        GameEvent.CallOnHealthChanged();
    }

    private void DecreaseHealthBy(int damage)
    {
        CurrentHealth -= damage;
        GameEvent.CallOnHealthChanged();
    }

    protected virtual void Die() => IsDead = true;

    #endregion
}
