using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Hồi máu")]
public class SkillCard_Heal : SkillCard
{
    [SerializeField] private float healthPercent;

    public override void ApplySkill(PlayerStats playerStats)
    {
        int healthAmount = Mathf.RoundToInt(playerStats.GetMaxHealthValue() * healthPercent);
        playerStats.Heal(healthAmount);
    }
}
