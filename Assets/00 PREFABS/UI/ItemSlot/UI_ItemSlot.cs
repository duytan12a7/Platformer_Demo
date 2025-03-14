using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    public InventoryItem inventoryItem;

    public void UpdateSlot(InventoryItem _newItem)
    {
        inventoryItem = _newItem;
        itemImage.color = Color.white;

        if (inventoryItem == null) return;

        itemImage.sprite = inventoryItem.data.ItemIcon;
        if (inventoryItem.stackSize > 1)
            itemText.text = inventoryItem.stackSize.ToString();
        else
            itemText.text = "";
    }

    public void CleanUpSlot()
    {
        inventoryItem = null;
        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (inventoryItem.data.ItemType == ItemType.Equipment)
            Inventory.Instance.EquipItem(inventoryItem.data);
    }
}
