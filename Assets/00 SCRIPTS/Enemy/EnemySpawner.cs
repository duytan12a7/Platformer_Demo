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
    private bool spawning = false;
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
            spawning = true;
            yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));

            while (aliveEnemies.Count > 0)
            {
                yield return null;
            }

            currentWaveIndex++;
        }

        // Hiện thông báo khi đã spawn hết tất cả đợt quái
        Debug.Log("Đã spawn hết tất cả đợt quái");
        sceneTrigger.SetActive(true);
    }

    IEnumerator SpawnWave(Wave wave)
    {
        foreach (var spawnData in wave.enemies)
        {
            yield return new WaitForSeconds(spawnData.delay);
            GameObject enemy = PoolManager.Instance.SpawnObject(spawnData.enemyPrefab);
            enemy.transform.position = spawnData.spawnPoint.position;
            enemy.SetActive(true);

            aliveEnemies.Add(enemy);
            enemy.GetComponentInChildren<EnemyStats>().OnDeath += () => aliveEnemies.Remove(enemy);
        }

        spawning = false;
    }
}
