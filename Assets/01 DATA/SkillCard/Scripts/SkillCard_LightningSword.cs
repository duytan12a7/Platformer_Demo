using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Lightning Sword")]
public class SkillCard_LightningSword : SkillCard
{
    [SerializeField] private int value;

    public override void ApplySkill(PlayerStats playerStats)
    {
        playerStats.PhysicalDamage.AddModifier(value);
        playerStats.HasElectricStrike = true;
    }
}
