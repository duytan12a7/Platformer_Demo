using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public static PickupManager Instance;
    private List<ItemObject> droppedItems = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void AddItem(ItemObject item) => droppedItems.Add(item);

    public void CollectAllItems()
    {
        foreach (ItemObject item in droppedItems)
        {
            if (item != null)
                item.Collect();
        }
        droppedItems.Clear();
    }
}
