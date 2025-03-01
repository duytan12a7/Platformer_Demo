using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttackState : EnemyState
{
    private Goblin _enemy;

    public GoblinAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Goblin enemy) : base(enemy, stateMachine, animBoolName)
    {
        _enemy = enemy;
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

        _enemy.SetZeroVelocity();

        if (_enemy.skeletonAnimation != null && _enemy.skeletonAnimation.AnimationState.GetCurrent(0).IsComplete
       || isAnimationFinished)
            stateMachine.ChangeState(_enemy.IdleState);
    }
}
