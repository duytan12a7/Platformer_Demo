using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour
{


    [Serializable]
    public class EnemyGroup
    {
        public GameObject[] enemyPrefabs;
        public int minSpawnCount;
        public int maxSpawnCount;
    }

    [SerializeField]
    private EnemyGroup[] enemyGroups;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private bool deactiveOnAwake = true;

    private int remainingEnemies;
    private List<GameObject> waveEnemies;
    private System.Random randomGenerator;

    private void Awake()
    {
        randomGenerator = new System.Random();
        InitializeWave();
    }

    private void InitializeWave()
    {
        waveEnemies = new List<GameObject>();
        SpawnAllEnemyGroups();

        remainingEnemies = waveEnemies.Count;

        if (deactiveOnAwake) SetEnemiesActiveState(false);
    }

    private void SpawnAllEnemyGroups()
    {
        foreach (var group in enemyGroups)
        {
            int spawnCount = randomGenerator.Next(
                group.minSpawnCount,
                group.maxSpawnCount + 1
            );

            for (int i = 0; i < spawnCount; i++)
            {
                GameObject enemyPrefab = group.enemyPrefabs[
                    randomGenerator.Next(group.enemyPrefabs.Length)
                ];

                int spawnIndex = (0 + i) % spawnPoints.Length;
                Transform spawnPoint = spawnPoints[spawnIndex];

                GameObject spawnedEnemy = Instantiate(
                    enemyPrefab,
                    spawnPoint.position,
                    Quaternion.identity,
                    transform
                );

                waveEnemies.Add(spawnedEnemy);
            }
        }
    }

    private void SetEnemiesActiveState(bool isActive)
    {
        foreach (var enemy in waveEnemies)
        {
            enemy.SetActive(isActive);
        }
    }

    public void OnEnemyDied(GameObject enemy)
    {
        waveEnemies.Remove(enemy);
        remainingEnemies--;

        if (remainingEnemies <= 0)
        {
            Debug.Log("Clear Wave");
        }
    }
}