using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}


[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType EquipmentType;

    [Header(" Major Stats ")]
    public int Strength;
    public int Agility;
    public int Intelligence;
    public int Vitality;

    [Header(" Offensive Stats ")]
    public int Damage;
    public int CriticalChance;
    public int CriticalPower;

    [Header(" Defensive Stats ")]
    public int Health;
    public int Armor;
    public int Evasion;
    public int MagicResistance;

    [Header(" Magic Stats ")]
    public int FireDamage;
    public int IceDamage;
    public int LightningDamage;

    public void AddModifier()
    {
        PlayerStats playerStats = GameManager.Instance.Player.Stats;

        playerStats.Strength.AddModifier(Strength);
        playerStats.Agility.AddModifier(Agility);
        playerStats.Intelligence.AddModifier(Intelligence);
        playerStats.Vitality.AddModifier(Vitality);

        playerStats.PhysicalDamage.AddModifier(Damage);
        playerStats.CriticalChance.AddModifier(CriticalChance);
        playerStats.CriticalPower.AddModifier(CriticalPower);

        playerStats.MaxHealth.AddModifier(Health);
        playerStats.Armor.AddModifier(Armor);
        playerStats.Evasion.AddModifier(Evasion);
        playerStats.MagicResistance.AddModifier(MagicResistance);

        playerStats.FireDamage.AddModifier(FireDamage);
        playerStats.IceDamage.AddModifier(IceDamage);
        playerStats.LightningDamage.AddModifier(LightningDamage);

    }

    public void RemoveModifier()
    {
        PlayerStats playerStats = GameManager.Instance.Player.Stats;

        playerStats.Strength.RemoveModifier(Strength);
        playerStats.Agility.RemoveModifier(Agility);
        playerStats.Intelligence.RemoveModifier(Intelligence);
        playerStats.Vitality.RemoveModifier(Vitality);

        playerStats.PhysicalDamage.RemoveModifier(Damage);
        playerStats.CriticalChance.RemoveModifier(CriticalChance);
        playerStats.CriticalPower.RemoveModifier(CriticalPower);

        playerStats.MaxHealth.RemoveModifier(Health);
        playerStats.Armor.RemoveModifier(Armor);
        playerStats.Evasion.RemoveModifier(Evasion);
        playerStats.MagicResistance.RemoveModifier(MagicResistance);

        playerStats.FireDamage.RemoveModifier(FireDamage);
        playerStats.IceDamage.RemoveModifier(IceDamage);
        playerStats.LightningDamage.RemoveModifier(LightningDamage);
    }
}
