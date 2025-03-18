using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Increase Damage")]
public class SkillCard_IncreaseDamage : SkillCard
{
    [SerializeField] private int value;

    public override void ApplySkill(PlayerStats playerStats)
    {
        playerStats.PhysicalDamage.AddModifier(value);
    }
}
