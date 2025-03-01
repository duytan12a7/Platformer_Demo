using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttackState : EnemyState
{
    private Goblin enemy;

    public GoblinAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Goblin enemy) : base(enemy, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
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
