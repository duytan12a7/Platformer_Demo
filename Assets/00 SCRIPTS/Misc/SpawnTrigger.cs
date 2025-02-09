using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private SpawnEnemy spawnEnemy;

    private void Start()
    {
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spawnEnemy.TriggerSpawn(transform);
        }
    }
}
