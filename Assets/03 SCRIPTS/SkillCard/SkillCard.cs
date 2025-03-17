using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Card", menuName = "Data/Skill Card")]
public class SkillCard : ScriptableObject
{
    public string skillName;
    public string description;
    public Sprite skillIcon;
    public SkillType skillType;

    public virtual void ApplySkill(PlayerStats playerStats)
    {
        
    }
}

public enum SkillType
{
    Fire, Ice, Poison, Lightning, Normal
}
