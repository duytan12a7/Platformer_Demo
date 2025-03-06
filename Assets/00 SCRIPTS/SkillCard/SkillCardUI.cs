using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillCardUI : MonoBehaviour
{
    [SerializeField] private Text skillNameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Image skillImage;
    [SerializeField] private Image typeImage;

    [SerializeField] private Sprite fireSprite;
    [SerializeField] private Sprite iceSprite;
    [SerializeField] private Sprite poisonSprite;
    [SerializeField] private Sprite lightningSprite;
    [SerializeField] private Sprite normalSprite;

    private readonly Dictionary<string, Sprite> skillTypeSprites = new();

    private void Awake()
    {
        skillTypeSprites["Fire"] = fireSprite;
        skillTypeSprites["Ice"] = iceSprite;
        skillTypeSprites["Poison"] = poisonSprite;
        skillTypeSprites["Lightning"] = lightningSprite;
        skillTypeSprites["Normal"] = normalSprite;
    }

    public void SetCardInfo(string skillName, string description, Sprite skillSprite, string skillType)
    {
        skillNameText.text = skillName;
        descriptionText.text = description;
        skillImage.sprite = skillSprite;

        typeImage.sprite = skillTypeSprites.ContainsKey(skillType) ? skillTypeSprites[skillType] : typeImage.sprite;
    }
}
