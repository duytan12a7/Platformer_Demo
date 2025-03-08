using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public static PickupManager Instance { get; private set; }
    private List<ItemObject> droppedItems = new();
    [SerializeField] private float delayCollect = 2f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterDroppedItem(ItemObject item) => droppedItems.Add(item);

    public void ScheduleAutoCollect() => Invoke(nameof(AutoCollectItems), delayCollect);

    private void AutoCollectItems()
    {
        foreach (ItemObject item in droppedItems)
        {
            if (item != null)
                item.Collect();
        }
        droppedItems.Clear();
    }
}
