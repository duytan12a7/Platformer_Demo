using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card/Increase Health")]
public class SkillCard_IncreaseHealth : SkillCard
{
    [SerializeField] private float healthPercent;

    public override void ApplySkill(PlayerStats playerStats)
    {
        int healthAmount = Mathf.RoundToInt(playerStats.GetMaxHealthValue() * healthPercent);
        playerStats.MaxHealth.AddModifier(healthAmount);
        playerStats.Heal(healthAmount);
    }
}
