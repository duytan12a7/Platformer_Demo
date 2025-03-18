using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Kiếm Điện")]
public class SkillCard_KiemDien : SkillCard
{
    [SerializeField] private int atk = 5;

    public override void ApplySkill(PlayerStats playerStats)
    {
        playerStats.AddModifyPhysicalDamage(atk);
        playerStats.HasElectricStrike = true;
    }
}
