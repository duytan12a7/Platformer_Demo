using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Increase Critical Rate")]
public class SkillCard_IncreaseCriticalRate : SkillCard
{
    [SerializeField] private int value;

    public override void ApplySkill(PlayerStats playerStats)
    {
        playerStats.CriticalChance.AddModifier(value);
    }
}
