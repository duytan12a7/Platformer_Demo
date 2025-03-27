using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Increase Armor")]
public class SkillCard_IncreaseArmor : SkillCard
{
    [SerializeField] private int value;

    public override void ApplySkill(PlayerStats playerStats)
    {
        playerStats.Armor.AddModifier(value);
    }
}
