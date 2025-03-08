using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{

    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.GetComponentInChildren<EnemyDamageSender>().ClearAttackedEnemies();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.SetZeroVelocity();

        if (enemy.skeletonAnimation != null && enemy.skeletonAnimation.AnimationState.GetCurrent(0).IsComplete
       || isAnimationFinished)
            stateMachine.ChangeState(enemy.IdleState);
    }
}
