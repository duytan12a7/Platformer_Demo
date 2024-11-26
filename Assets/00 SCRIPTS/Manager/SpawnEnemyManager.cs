using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour
{

    [System.Serializable]
    public class EnemyGroup
    {
        public GameObject[] enemyPrefabs;
        public int minSpawnCount;
        public int maxSpawnCount;
    }

    [SerializeField] private EnemyGroup[] enemyGroups;

    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        SpawnAllEnemyGroups();
    }

    private void SpawnAllEnemyGroups()
    {
        foreach (EnemyGroup group in enemyGroups)
        {
            int spawnCount = Random.Range(
                group.minSpawnCount,
                group.maxSpawnCount
            );

            SpawnAllEnemyGroup(group, spawnCount);
        }
    }

    private void SpawnAllEnemyGroup(EnemyGroup group, int spawnCount)
    {
        Vector2 lastSpawnPosition = Vector2.zero;

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject enemyPrefab = GetRandomEnemyPrefab(group);

            Transform spawnPoint = GetSpawnPoint(i);

            Vector2 spawnPosition = GetRandomSpawnPosition(spawnPoint, lastSpawnPosition);
            lastSpawnPosition = spawnPosition;

            GameObject spawnedEnemy = PoolManager.Instance.SpawnObject(enemyPrefab);
            spawnedEnemy.transform.SetLocalPositionAndRotation(spawnPosition, Quaternion.identity);
            spawnedEnemy.SetActive(true);
        }
    }

    private GameObject GetRandomEnemyPrefab(EnemyGroup group)
    {
        int prefabIndex = Random.Range(0, group.enemyPrefabs.Length);
        return group.enemyPrefabs[prefabIndex];
    }

    private Transform GetSpawnPoint(int index) => spawnPoints[index % spawnPoints.Length];

    private Vector2 GetRandomSpawnPosition(Transform spawnPoint, Vector2 lastPosition)
    {
        Vector2 spawnPosition;

        do
        {
            spawnPosition = new Vector2(
                Random.Range(spawnPoint.position.x - 3f, spawnPoint.position.x + 3f),
                spawnPoint.position.y
            );
        } while (Vector2.Distance(lastPosition, spawnPosition) < 0.5f);

        return spawnPosition;
    }
}