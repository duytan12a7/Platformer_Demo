using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Tăng Máu")]
public class SkillCard_IncreHealth : SkillCard
{
    [SerializeField] private float value;

    public override void ApplySkill(PlayerStats playerStats)
    {
        playerStats.IncreaseHealth(value);
    }
}
