using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunnedState : EnemyState
{

    public EnemyStunnedState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.PlayAnimation(animBoolName, false);
        stateTimer = enemy.StunDuration;
        enemy.CloseCounterAttackWindow();

        enemy.StartCoroutine(enemy.EntityFX.FlashFX());

        // enemy.EntityFX.InvokeRepeating("RedColorBlink", 0, .1f);

        rb.velocity = new Vector2(-enemy.FacingDirection * enemy.StunDirection.x, enemy.StunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();

        // enemy.EntityFX.Invoke("CancelColorChange", 0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.IdleState);
    }
}
