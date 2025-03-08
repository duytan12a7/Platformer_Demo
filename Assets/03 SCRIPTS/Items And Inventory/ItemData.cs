using UnityEngine;


public enum ItemType
{
    Gold,
    Material,
    Equipment,
    Experience
}


[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType ItemType;
    public string ItemName;
    public int ItemValue;
    public Sprite ItemIcon;

    [Range(0, 100)]
    public float DropChance;
}
