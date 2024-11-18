using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : EnemyState
{
    private Enemy_Skeleton enemy;

    public SkeletonIdleState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, enemyData, animBoolName, skeleton)
    {
        this.enemy = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemyData.IdleTimer;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.MoveState);
    }
}
