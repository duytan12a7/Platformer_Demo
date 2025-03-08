using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyDamageSender : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private HashSet<PlayerStats> attackedEnemies = new();

    private void Start()
    {
        LoadComponent();
    }

    private void LoadComponent()
    {
        if (enemy != null) return;

        enemy = GetComponentInParent<Enemy>();
        Debug.Log(transform.name + " : LoadEnemy", gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerStats target = collider?.GetComponentInChildren<PlayerStats>();
        if (target == null || attackedEnemies.Contains(target)) return;

        enemy.Stats.DoDamage(target, transform);
        attackedEnemies.Add(target);
    }

    public void ClearAttackedEnemies() => attackedEnemies.Clear();
}
