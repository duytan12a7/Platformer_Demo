using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnSetting
{
    public Enemy enemy;
    public int maxCount;
    public int startPos;
}

public class SpawnManager : MonoBehaviour
{
    [Header("Danh sách cài đặt Spawn")]
    public List<SpawnSetting> spawnSettings = new();

    [Header("Danh sách vị trí Spawn")]
    public List<Transform> enemyTrans = new();
    private Dictionary<Enemy, int> enemySpawnCounts = new();

    void Start()
    {
        InitializeSpawnCounts();
        SpawnEnemies();
    }

    private void InitializeSpawnCounts()
    {
        foreach (var setting in spawnSettings)
        {
            if (!enemySpawnCounts.ContainsKey(setting.enemy))
            {
                enemySpawnCounts[setting.enemy] = 0;
            }
        }
    }

    private void SpawnEnemies()
    {
        foreach (var setting in spawnSettings)
        {
            SpawnFunction(setting.enemy, setting.maxCount, enemyTrans, setting.startPos);
        }
    }

    private void SpawnFunction(Enemy enemy, int spawnCountMax, List<Transform> trans, int pos)
    {
        if (pos < 0 || pos >= trans.Count) return;

        int currentCount = enemySpawnCounts[enemy];
        int spawnAmount = Mathf.Max(0, spawnCountMax - currentCount);

        Vector2 randomPos;
        Vector2 currentPos = Vector2.zero;

        for (int i = 0; i < spawnAmount; i++)
        {
            int spawnIndex = (pos + i) % trans.Count;
            Transform spawnPosition = trans[spawnIndex];
            do
            {
                randomPos = new(spawnPosition.position.x + Random.Range(-5f, 5f), spawnPosition.position.y);
            }
            while (Mathf.Abs(randomPos.x - currentPos.x) < 1f);

            Enemy newEnemy = Instantiate(enemy, randomPos, spawnPosition.rotation);
            newEnemy.gameObject.SetActive(true);
            currentPos = randomPos;

            enemySpawnCounts[enemy]++;
        }
    }
}
