using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private static SpawnEnemy instance;
    public static SpawnEnemy Instance => instance;

    [System.Serializable]
    public class EnemyData
    {
        public GameObject enemyPrefab;
        public int minSpawnCount;
        public int maxSpawnCount;
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public EnemyData[] enemyDataList;
    }

    [System.Serializable]
    public class SpawnPoint
    {
        public Transform spawnLocation;
        public EnemyGroup enemyGroup;
        public bool hasSpawned = false;
    }

    [SerializeField] private SpawnPoint[] spawnPoints;
    private Dictionary<Transform, List<GameObject>> spawnedEnemies = new();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach (var point in spawnPoints)
        {
            spawnedEnemies[point.spawnLocation] = new List<GameObject>();
        }
    }

    public void TriggerSpawn(Transform spawnLocation)
    {
        foreach (SpawnPoint point in spawnPoints)
        {
            if (point.spawnLocation == spawnLocation && !point.hasSpawned)
            {
                SpawnEnemiesAtPoint(point);
                point.hasSpawned = true;
            }
        }
    }

    private void SpawnEnemiesAtPoint(SpawnPoint point)
    {
        List<GameObject> enemiesList = spawnedEnemies[point.spawnLocation];

        foreach (EnemyData enemyData in point.enemyGroup.enemyDataList)
        {
            int spawnCount = Random.Range(enemyData.minSpawnCount, enemyData.maxSpawnCount + 1);

            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition(point.spawnLocation);
                GameObject spawnedEnemy = PoolManager.Instance.SpawnObject(enemyData.enemyPrefab);
                spawnedEnemy.transform.position = spawnPosition;
                spawnedEnemy.SetActive(true);
                enemiesList.Add(spawnedEnemy);
            }
        }
    }

    private Vector3 GetRandomSpawnPosition(Transform spawnPoint)
    {
        return new Vector3(
            Random.Range(spawnPoint.position.x - 3f, spawnPoint.position.x + 3f),
            spawnPoint.position.y,
            spawnPoint.position.z
        );
    }

    public void ResetAllSpawns()
    {
        foreach (var point in spawnPoints)
        {
            point.hasSpawned = false;
            foreach (var enemy in spawnedEnemies[point.spawnLocation])
            {
                if (enemy != null)
                {
                    enemy.SetActive(false); // Không Destroy, chỉ Disable để sử dụng lại Pool
                }
            }
            spawnedEnemies[point.spawnLocation].Clear();
        }
    }
}
