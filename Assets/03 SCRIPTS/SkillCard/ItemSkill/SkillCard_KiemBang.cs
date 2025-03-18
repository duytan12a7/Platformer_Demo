using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Kiếm Băng")]
public class SkillCard_KiemBang : SkillCard
{
    [SerializeField] private int atk = 5;

    public override void ApplySkill(PlayerStats playerStats)
    {
        playerStats.AddModifyPhysicalDamage(atk);
        playerStats.HasIceStrike = true;
    }
}
