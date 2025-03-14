using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;
    public static PoolManager Instance => instance;

    private Dictionary<GameObject, GameObject> poolParents = new();
    private Dictionary<GameObject, List<GameObject>> pooledObjects = new();

    private void Awake()
    {
        if (instance != null || instance == this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public GameObject SpawnObject(GameObject objectKey)
    {
        GameObject poolParent = GetOrCreatePool(objectKey);

        GameObject availableObject = GetAvailablePooledObject(objectKey);
        if (availableObject != null)
            return availableObject;

        return CreateNewPooledObject(objectKey, poolParent);
    }

    public T SpawnObject<T>(T component) where T : Component
    {

        GameObject objectKey = component.gameObject;

        GameObject poolParent = GetOrCreatePool(objectKey);

        GameObject availableObject = GetAvailablePooledObject(objectKey);
        if (availableObject != null)
            return availableObject.GetComponent<T>();

        return CreateNewPooledObject(objectKey, poolParent).GetComponent<T>();
    }

    private GameObject GetOrCreatePool(GameObject objectKey)
    {

        if (!poolParents.TryGetValue(objectKey, out GameObject poolParent))
        {
            poolParent = new($"{objectKey.name}Pool");
            poolParent.transform.parent = transform;
            poolParents[objectKey] = poolParent;

            pooledObjects[objectKey] = new();
        }
        return poolParent;
    }

    private GameObject GetAvailablePooledObject(GameObject objectKey)
    {
        foreach (GameObject pooledObject in pooledObjects[objectKey])
        {
            if (pooledObject.activeSelf)
                continue;
            return pooledObject;
        }

        return null;
    }

    private GameObject CreateNewPooledObject(GameObject objectKey, GameObject poolParent)
    {
        GameObject newPooled = Instantiate(objectKey, poolParent.transform);
        newPooled.name = objectKey.name;
        // newPooled.SetActive(false);

        pooledObjects[objectKey].Add(newPooled);
        return newPooled;
    }
}
