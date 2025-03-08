using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillSelectionUI : MonoBehaviour
{
    [SerializeField] private SkillManager skillManager;
    public GameObject skillSelectionPanel;
    public Button[] skillButtons;
    private List<SkillCard> currentSkills;
    [SerializeField] private SkillCardUI[] skillCards;

    public void ShowSkillSelection(List<SkillCard> skills)
    {
        skillSelectionPanel.SetActive(true);
        currentSkills = skills;

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (i < skills.Count)
            {
                skillButtons[i].gameObject.SetActive(true);
                skillCards[i].SetCardInfo(skills[i].skillName, skills[i].description, skills[i].skillIcon, skills[i].skillType.ToString());

                int index = i;
                skillButtons[i].onClick.RemoveAllListeners();
                skillButtons[i].onClick.AddListener(() => SelectSkill(index));
            }
            else
                skillButtons[i].gameObject.SetActive(false);
        }
    }

    public void SelectSkill(int index)
    {
        skillManager.AddSkill(currentSkills[index]);
        skillSelectionPanel.SetActive(false);
    }
}