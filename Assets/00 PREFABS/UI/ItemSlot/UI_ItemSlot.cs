using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TextMeshProUGUI itemText;

    public InventoryItem item;

    public virtual void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;
        itemImage.color = Color.white;

        if (item == null) return;
        itemImage.sprite = item.data.ItemIcon;
        if (item.stackSize > 1)
            itemText.text = item.stackSize.ToString();
        else
            itemText.text = "";
    }

    public virtual void CleanUpSlot()
    {
        item = null;
        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (item.data.ItemType == ItemType.Equipment)
            Inventory.Instance.EquipItem(item.data);
    }
}
