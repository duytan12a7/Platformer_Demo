using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Tăng Tỉ Lệ Chí Mạng")]
public class SkillCard_IncreCritical : SkillCard
{
    [SerializeField] private int value;

    public override void ApplySkill(PlayerStats playerStats)
    {
        playerStats.AddModifyCriticalChance(value);
    }
}
