using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private ItemData itemData;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemData.Icon;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Pickup Item " + itemData.ItemName);
        gameObject.SetActive(false);
    }
}
