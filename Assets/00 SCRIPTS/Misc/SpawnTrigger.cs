using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private SpawnEnemyManager spawnEnemy;

    private void Start()
    {
        spawnEnemy = GetComponentInParent<SpawnEnemyManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spawnEnemy.TriggerSpawn(transform);
        }
    }
}
