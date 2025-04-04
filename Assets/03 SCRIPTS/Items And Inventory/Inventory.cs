using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    [Header(" UI Inventory ")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;

    private UI_ItemSlot[] inventoryItemSlot;
    private UI_ItemSlot[] stashItemSlot;
    private UI_EquipmentSlot[] equipmentItemSlot;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        stashItemSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentItemSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
    }

    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;
        InventoryItem newItem = new(newEquipment);

        ItemData_Equipment oldEquipment = null;
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.EquipmentType == newEquipment.EquipmentType)
                oldEquipment = item.Key;
        }
        if (oldEquipment != null)
        {
            UnEquipItem(oldEquipment);
            // AddItem(oldEquipment);
        }

        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifier();

        RemoveItem(_item);

        UpdateSlotUI();
    }

    public void UnEquipItem(ItemData_Equipment itemToRemove)
    {
        if (equipmentDictionary.TryGetValue(itemToRemove, out InventoryItem item))
        {
            AddItem(itemToRemove);
            equipment.Remove(item);
            equipmentDictionary.Remove(itemToRemove);
            itemToRemove.RemoveModifier();
        }
    }

    private void UpdateSlotUI()
    {
        for (int i = 0; i < equipmentItemSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.EquipmentType == equipmentItemSlot[i].slotType)
                    equipmentItemSlot[i].UpdateSlot(item.Value);
            }
        }
        CleanUpItemSlot();
        UpdateItemSlot();
    }

    private void UpdateItemSlot()
    {
        for (int i = 0; i < inventory.Count; i++)
            inventoryItemSlot[i].UpdateSlot(inventory[i]);

        for (int i = 0; i < stash.Count; i++)
            stashItemSlot[i].UpdateSlot(stash[i]);
    }

    private void CleanUpItemSlot()
    {
        for (int i = 0; i < inventoryItemSlot.Length; i++)
            inventoryItemSlot[i].CleanUpSlot();

        for (int i = 0; i < stashItemSlot.Length; i++)
            stashItemSlot[i].CleanUpSlot();
    }

    public void AddItem(ItemData _item)
    {
        if (_item.ItemType == ItemType.Equipment)
            AddToInventory(_item);
        else if (_item.ItemType == ItemType.Material)
            AddToStash(_item);

        UpdateSlotUI();
    }

    private void AddToStash(ItemData _item)
    {
        if (stashDictionary.TryGetValue(_item, out InventoryItem item))
            item.AddStack();
        else
        {
            InventoryItem newItem = new(_item);
            stash.Add(newItem);
            stashDictionary.Add(_item, newItem);
        }
    }

    private void AddToInventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem item))
            item.AddStack();
        else
        {
            InventoryItem newItem = new(_item);
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }
    }

    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem inventoryItem))
        {
            if (inventoryItem.stackSize <= 1)
            {
                inventory.Remove(inventoryItem);
                inventoryDictionary.Remove(_item);
            }
            else
                inventoryItem.RemoveStack();
        }

        if (stashDictionary.TryGetValue(_item, out InventoryItem stashItem))
        {
            if (stashItem.stackSize <= 1)
            {
                stash.Remove(stashItem);
                stashDictionary.Remove(_item);
            }
            else
                stashItem.RemoveStack();
        }

        UpdateSlotUI();
    }
}
