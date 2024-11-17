using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void AnimationFinishTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        enemy.AnimationTriggerEvent(triggerType);
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius, enemy.whatIsCharacter);

        if (colliders.Length == 0) return;

        foreach (Collider2D hit in colliders)
        {
            PlayerStats target = hit.GetComponentInChildren<PlayerStats>();
            if (target == null) continue;

            enemy.Stats.DoDamage(target);
        }
    }
}
