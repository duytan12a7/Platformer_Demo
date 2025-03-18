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

    public void AnimationEventTrigger(string eventName)
    {
        switch (eventName)
        {
            case "lock_action":
                player.IsMove = false;
                player.SetZeroVelocity();
                break;
            case "move_combo":
                player.SetZeroVelocity();
                if (player.StateMachine.CurrentState is PlayerAttackState attackState)
                {
                    float moveDistance = player.playerData.AttackMovement[attackState.comboCounter].x;
                    player.SetVelocity(moveDistance * player.FacingDirection, player.CurrentVelocity.y);
                }
                break;
            case "unlock_action":
                player.IsMove = true;
                break;
            default:
                break;
        }
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius, player.whatIsCharacter);
        if (colliders.Length == 0) return;

        foreach (Collider2D hit in colliders)
        {
            EnemyStats target = hit.GetComponentInChildren<EnemyStats>();
            if (target == null) continue;
            player.Stats.DoDamage(target, player.transform);
        }
    }
}
