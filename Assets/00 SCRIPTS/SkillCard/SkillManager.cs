using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private static SkillManager instance;
    public static SkillManager Instance => instance;

    public List<SkillCard> availableSkills = new();
    public List<SkillCard> chosenSkills = new();

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
        availableSkills.Remove(skill);
    }

    public List<SkillCard> GetRandomSkills(int count)
    {
        List<SkillCard> randomSkills = new();
        List<SkillCard> copyList = new List<SkillCard>(availableSkills);

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
            case SkillEffect.ElectricStrike:
                // Tăng sát thương sét
                break;
            case SkillEffect.Shield:
                // Cung cấp lá chắn bảo vệ
                break;
        }
    }

}
