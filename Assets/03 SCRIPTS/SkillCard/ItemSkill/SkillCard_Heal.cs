using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Hồi máu")]
public class SkillCard_Heal : SkillCard
{
    [SerializeField] private float value;

    public override void ApplySkill(PlayerStats playerStats)
    {
        playerStats.Heal(value);
    }
}
