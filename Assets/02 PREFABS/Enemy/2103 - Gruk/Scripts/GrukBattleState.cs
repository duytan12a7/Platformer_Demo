using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrukBattleState : EnemyBattleState
{
    private int lastAttackType = 2;

    public GrukBattleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.BattleTime;
            if (enemy.CheckAttackDistance())
            {
                // enemy.StateMachine.ChangeState(enemy.AttackState);
                enemy.StateMachine.ChangeState(new GrukAttackState(enemy, enemy.StateMachine, "idle", lastAttackType));
                lastAttackType = (lastAttackType == 1) ? 2 : 1;
            }
        }
        else if (stateTimer < 0 || Vector2.Distance(playerTransform.position, enemy.transform.position) > 7)
        {
            stateMachine.ChangeState(enemy.IdleState);
        }

        float directionX = (playerTransform.position.x - enemy.transform.position.x) > 0 ? 1 : -1;
        enemy.SetVelocityX(directionX * enemy.MoveSpeed);
        enemy.CheckFlip(directionX);
    }
}