using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = playerData.CounterAttackDuration;
        player.Anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetZeroVelocity();

        AttackStunned();

        if (stateTimer < 0 || isAnimationFinished)
            stateMachine.ChangeState(player.IdleState);
    }

    private void AttackStunned()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius, player.whatIsCharacter);
        if (colliders.Length == 0) return;

        foreach (Collider2D hit in colliders)
        {
            Enemy target = hit.GetComponent<Enemy>();
            if (target == null) continue;
            if (target.CanBeStunned())
            {
                stateTimer = 10;
                player.Anim.SetBool("SuccessfulCounterAttack", true);
            }
        }
    }
}
