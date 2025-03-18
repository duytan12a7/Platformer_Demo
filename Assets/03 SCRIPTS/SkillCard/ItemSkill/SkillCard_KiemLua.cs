using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Kiếm Lửa")]
public class SkillCard_KiemLua : SkillCard
{
    [SerializeField] private int atk = 5;

    public override void ApplySkill(PlayerStats playerStats)
    {
        playerStats.AddModifyPhysicalDamage(atk);
        playerStats.HasFireStrike = true;
    }
}
