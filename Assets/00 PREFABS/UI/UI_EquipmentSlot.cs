using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType slotType;
    [SerializeField] private Image bgItem;

    private void OnValidate()
    {
        gameObject.name = "Equipment Slot - " + slotType.ToString();
    }

    public override void UpdateSlot(InventoryItem _newItem)
    {
        base.UpdateSlot(_newItem);
        bgItem.gameObject.SetActive(true);
        itemImage.gameObject.SetActive(true);
    }

    public override void CleanUpSlot()
    {
        base.CleanUpSlot();
        bgItem.gameObject.SetActive(false);
        itemImage.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        Inventory.Instance.UnEquipItem(item.data as ItemData_Equipment);
        CleanUpSlot();
    }
}
