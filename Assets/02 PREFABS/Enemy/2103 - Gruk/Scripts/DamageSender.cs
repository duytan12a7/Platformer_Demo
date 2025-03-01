using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Hit:::: " + collider.name);
        PlayerStats target = collider?.GetComponentInChildren<PlayerStats>();
        if (target == null || enemy.isSkillAttackActive) return;

        enemy.Stats.PerformAttack(target, transform);

        enemy.isSkillAttackActive = true;
    }
}
