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
        if (playerStats.OwnedSkills.Contains(skill))
        {
            Debug.Log("Đã sở hữu kỹ năng này, có thể nâng cấp!");
            return;
        }

        playerStats.OwnedSkills.Add(skill);
        skill.ApplySkill(playerStats);
        AvailableSkills.Remove(skill);
    }

    public List<SkillCard> GetRandomSkills(int count)
    {
        List<SkillCard> randomSkills = new();
        List<SkillCard> copyList = new(AvailableSkills);

        bool hasFireStrike = playerStats.HasFireStrike;
        bool hasIceStrike = playerStats.HasIceStrike;
        bool hasElectricStrike = playerStats.HasElectricStrike;

        if (hasFireStrike)
            copyList.RemoveAll(skill => skill is SkillCard_KiemBang || skill is SkillCard_KiemDien);
        else if (hasIceStrike)
            copyList.RemoveAll(skill => skill is SkillCard_KiemLua || skill is SkillCard_KiemDien);
        else if (hasElectricStrike)
            copyList.RemoveAll(skill => skill is SkillCard_KiemLua || skill is SkillCard_KiemBang);

        // Random Skill từ danh sách đã lọc
        for (int i = 0; i < count; i++)
        {
            if (copyList.Count == 0) break;

            int index = Random.Range(0, copyList.Count);
            randomSkills.Add(copyList[index]);
            copyList.RemoveAt(index);
        }

        return randomSkills;
    }

}
