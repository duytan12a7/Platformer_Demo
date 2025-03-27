using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnData
    {
        public GameObject enemyPrefab;
        public Transform spawnPoint;
        public float delay;
    }

    [System.Serializable]
    public class Wave
    {
        public List<SpawnData> enemies;
    }

    public List<Wave> waves;
    public float timeBetweenWaves = 0f;
    private int currentWaveIndex = 0;
    private List<GameObject> aliveEnemies = new List<GameObject>();

    [SerializeField] private GameObject sceneTrigger;

    void Start()
    {
        sceneTrigger.SetActive(false);
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < waves.Count)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));

            while (aliveEnemies.Count > 0)
            {
                yield return null;
            }

            currentWaveIndex++;
        }

        sceneTrigger.SetActive(true);
        PickupManager.Instance.ScheduleAutoCollect();
    }

    IEnumerator SpawnWave(Wave wave)
    {
        foreach (var spawnData in wave.enemies)
        {
            yield return new WaitForSeconds(spawnData.delay);
            GameObject enemy = PoolManager.Instance.SpawnObject(spawnData.enemyPrefab);
            enemy.transform.position = spawnData.spawnPoint.position;

            if (!enemy.activeSelf)
                enemy.SetActive(true);

            aliveEnemies.Add(enemy);
            enemy.GetComponentInChildren<EnemyStats>().OnDeath += () => aliveEnemies.Remove(enemy);
        }
    }
}
