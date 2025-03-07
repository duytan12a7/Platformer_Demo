using UnityEngine;


//TODO: Khi mo ra the da so huu thi upgrade level
[System.Serializable]
public class SkillCard
{
    public string skillName;
    public string description;
    public Sprite skillIcon;
    public SkillType skillType;
    public SkillEffect effectType;
    public float effectValue;
}

public enum SkillType
{
    Fire, Ice, Poison, Lightning, Normal
}

public enum SkillEffect
{
    IncreaseAttack,  // Tăng sát thương
    IncreaseDefense, // Tăng giáp
    LifeSteal,       // Hút máu
    CriticalBoost,   // Tăng chí mạng
    ElectricStrike,  // Kiếm Sấm Sét
    Shield,          // Nhận lá chắn
    FireStrike,
    IceStrike
}
