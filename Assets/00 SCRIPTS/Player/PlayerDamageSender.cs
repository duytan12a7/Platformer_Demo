using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDamageSender : MonoBehaviour
{
    [SerializeField] private Player player;
    private HashSet<EnemyStats> attackedEnemies = new HashSet<EnemyStats>();

    private void Start()
    {
        LoadComponent();
    }

    private void LoadComponent()
    {
        if (player != null) return;

        player = GetComponentInParent<Player>();
        Debug.Log(transform.name + " : LoadPlayer", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        EnemyStats target = collider?.GetComponentInChildren<EnemyStats>();
        if (target == null || attackedEnemies.Contains(target)) return;

        player.Stats.PerformAttack(target, transform);
        attackedEnemies.Add(target);
    }

    public void ClearAttackedEnemies() => attackedEnemies.Clear();
}
