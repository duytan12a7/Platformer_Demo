using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] private ItemData[] possibleDrop;
    private List<ItemData> dropList = new();

    [SerializeField] private GameObject dropPrefab;

    private void Awake() => enemy = GetComponent<Enemy>();

    private void Start() => enemy.Stats.OnDeath += GenerateDrop;

    public virtual void GenerateDrop()
    {
        dropList.Clear();

        for (int i = 0; i < 3; i++)
        {
            ItemData goldItem = GetItemByType(ItemType.Gold);
            if (goldItem != null)
                DropItem(goldItem);
        }

        ItemData expItem = GetItemByType(ItemType.Experience);
        if (expItem != null)
        {
            DropItem(expItem);
        }

        List<ItemData> filteredDropList = new();
        foreach (ItemData item in possibleDrop)
        {
            if (item.ItemType != ItemType.Gold && item.ItemType != ItemType.Experience)
                if (Random.Range(0, 100) <= item.DropChance)
                    filteredDropList.Add(item);
        }

        int randomItemCount = Random.Range(0, 2);

        for (int i = 0; i < randomItemCount; i++)
        {
            if (filteredDropList.Count > 0)
            {
                ItemData randomItem = filteredDropList[Random.Range(0, filteredDropList.Count)];
                DropItem(randomItem);
            }
        }
    }

    protected void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        Vector2 randomVelocity = new(Random.Range(-5, 5), Random.Range(5, 10));

        ItemObject itemObject = newDrop.GetComponent<ItemObject>();
        itemObject.SetupItem(_itemData, randomVelocity);
        PickupManager.Instance.AddItem(itemObject);
    }

    private ItemData GetItemByType(ItemType type)
    {
        foreach (ItemData item in possibleDrop)
        {
            if (item.ItemType == type)
                return item;
        }
        return null;
    }

    private void OnDestroy() => enemy.Stats.OnDeath -= GenerateDrop;
}
