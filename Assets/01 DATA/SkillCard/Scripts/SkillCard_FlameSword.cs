using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Flame Sword")]
public class SkillCard_FlameSword : SkillCard
{
    [SerializeField] private int value;

    public override void ApplySkill(PlayerStats playerStats)
    {
        playerStats.PhysicalDamage.AddModifier(value);
        playerStats.HasFireStrike = true;
    }
}