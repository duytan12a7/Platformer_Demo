using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour
{
    public static PickupManager Instance { get; private set; }
    private List<ItemObject> droppedItems = new();
    [SerializeField] private float delayCollect = 0.5f;
    [SerializeField] private float moveSpeed = 20f;
    private Transform playerTransform;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        playerTransform = GameManager.Instance.Player.transform;
    }

    public void RegisterDroppedItem(ItemObject item) => droppedItems.Add(item);

    public void ScheduleAutoCollect() => Invoke(nameof(AutoCollectItems), delayCollect);

    private void AutoCollectItems()
    {
        foreach (ItemObject item in droppedItems)
        {
            if (item != null)
                StartCoroutine(MoveItemToPlayer(item));
        }
        droppedItems.Clear();
    }

    private IEnumerator MoveItemToPlayer(ItemObject item)
    {
        Vector3 targetPos = new(playerTransform.position.x, playerTransform.position.y + 1.5f, playerTransform.position.z);
        while (item != null && Vector3.Distance(item.transform.position, targetPos) > 0.5f)
        {
            item.transform.position = Vector3.MoveTowards(item.transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        if (item != null)
            item.Collect();
    }
}