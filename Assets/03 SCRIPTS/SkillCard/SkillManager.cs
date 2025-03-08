using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private static SkillManager instance;
    public static SkillManager Instance => instance;

    public List<SkillCard> AvailableSkills = new();

    [SerializeField] private PlayerStats playerStats;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddSkill(SkillCard skill)
    {
        playerStats.OwnedSkills.Add(skill);
        ApplySkillEffects(skill);
        AvailableSkills.Remove(skill);
    }
    public List<SkillCard> GetRandomSkills(int count)
    {
        List<SkillCard> randomSkills = new();
        List<SkillCard> copyList = new(AvailableSkills);

        bool hasFireStrike = playerStats.HasFireStrike;
        bool hasIceStrike = playerStats.HasIceStrike;
        bool hasElectricStrike = playerStats.HasElectricStrike;

        if (hasFireStrike || hasIceStrike || hasElectricStrike)
        {
            copyList.RemoveAll(skill =>
                skill.effectType == SkillEffect.FireStrike ||
                skill.effectType == SkillEffect.IceStrike ||
                skill.effectType == SkillEffect.ElectricStrike
                );
        }

        for (int i = 0; i < count; i++)
        {
            if (copyList.Count == 0) break;

            int index = Random.Range(0, copyList.Count);
            randomSkills.Add(copyList[index]);
            copyList.RemoveAt(index);
        }
        return randomSkills;
    }


    public void ApplySkillEffects(SkillCard skill)
    {
        if (skill == null) return;

        switch (skill.effectType)
        {
            case SkillEffect.IncreaseAttack:
                playerStats.AddModifyPhysicalDamage((int)skill.effectValue);
                break;
            case SkillEffect.IncreaseDefense:
                playerStats.AddModifyArmor((int)skill.effectValue);
                break;
            case SkillEffect.LifeSteal:
                // Cập nhật giá trị hút máu
                break;
            case SkillEffect.CriticalBoost:
                playerStats.AddModifyCriticalChance((int)skill.effectValue);
                break;
            case SkillEffect.FireStrike:
                playerStats.HasFireStrike = true;
                break;
            case SkillEffect.IceStrike:
                playerStats.HasIceStrike = true;
                break;
            case SkillEffect.ElectricStrike:
                playerStats.HasElectricStrike = true;
                break;
                // case SkillEffect.PoisonStrike:
                //     playerStats.HasPoisonStrike = true;
                //     break;
        }
    }

}
