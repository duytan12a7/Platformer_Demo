using UnityEngine;
using UnityEngine.UI;

public class SkillCardUI : MonoBehaviour
{
    public Text skillNameText;
    public Text descriptionText;
    // public Image skillImage;

    public void SetCardInfo(string skillName, string description)
    {
        skillNameText.text = skillName;
        descriptionText.text = description;
    }
}
