using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Increase Evasion")]
public class SkillCard_IncreaseEvasion : SkillCard
{
    [SerializeField] private int value;

    public override void ApplySkill(PlayerStats playerStats)
    {
        playerStats.Evasion.AddModifier(value);
    }
}
