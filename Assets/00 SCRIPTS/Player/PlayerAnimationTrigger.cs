using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void AnimationFinishTrigger()
    {
        player.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius, player.whatIsCharacter);
        if (colliders.Length == 0) return;

        foreach (Collider2D hit in colliders)
        {
            EnemyStats target = hit.GetComponentInChildren<EnemyStats>();
            if (target == null) continue;
            player.Stats.PerformAttack(target);
        }
    }
}
