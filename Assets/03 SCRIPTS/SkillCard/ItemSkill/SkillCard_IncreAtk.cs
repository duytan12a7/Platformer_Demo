using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Tăng Sát Thương")]
public class SkillCard_IncreAtk : SkillCard
{
    [SerializeField] private int value;

    public override void ApplySkill(PlayerStats playerStats)
    {
        playerStats.AddModifyPhysicalDamage(value);
    }
}
