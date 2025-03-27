using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Ice Sword")]
public class SkillCard_IceSword : SkillCard
{
    [SerializeField] private int value;

    public override void ApplySkill(PlayerStats playerStats)
    {
        playerStats.PhysicalDamage.AddModifier(value);
        playerStats.HasIceStrike = true;
    }
}
