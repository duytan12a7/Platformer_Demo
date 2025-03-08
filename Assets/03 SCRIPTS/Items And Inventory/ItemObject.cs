using System;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ItemData itemData;

    private void SetupVisuals()
    {
        if (itemData == null)
            return;

        GetComponent<SpriteRenderer>().sprite = itemData.ItemIcon;
        gameObject.name = "Item object - " + itemData.ItemName;
    }


    public void SetupItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;

        SetupVisuals();
    }

    public void Collect()
    {
        switch (itemData.ItemType)
        {
            case ItemType.Gold:
                break;
            case ItemType.Experience:
                GameEvent.CallOnGainExp(itemData.ItemValue);
                break;
        }
        Destroy(gameObject);
    }
}
