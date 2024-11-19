using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnSetting
{
    public Enemy enemy;        // Đối tượng kẻ thù
    public int maxCount;       // Số lượng tối đa để spawn
    public int startPos;       // Vị trí bắt đầu spawn trong danh sách enemyTrans
}

public class MapManager : MonoBehaviour
{
    [Header("Danh sách cài đặt Spawn")]
    public List<SpawnSetting> spawnSettings = new(); // Danh sách spawn được chỉnh từ Inspector

    [Header("Danh sách vị trí Spawn")]
    public List<Transform> enemyTrans = new();

    public int spawnCount = 0; // Số lượng kẻ thù đã spawn

    void Start()
    {
        SpawnEnemies();
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
        if (pos < 0 || pos >= trans.Count)
        {
            Debug.LogWarning($"Invalid position index: {pos}. Skipping spawn for enemy {enemy.name}.");
            return;
        }

        int spawnAmount = Mathf.Max(0, spawnCountMax - spawnCount);
        Vector2 randomPos;
        Vector2 currentPos = Vector2.zero;
        for (int i = 0; i < spawnAmount; i++)
        {
            Transform spawnPosition = trans[Random.Range(pos, trans.Count)];
            do
            {
                randomPos = new(spawnPosition.position.x + Random.Range(-5f, 5f), spawnPosition.position.y);
            }
            while (Mathf.Abs(randomPos.x - currentPos.x) < 1f);

            Instantiate(enemy, randomPos, spawnPosition.rotation);
            currentPos = randomPos;
            spawnCount++;
        }
    }
}
