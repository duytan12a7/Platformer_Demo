using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    private Enemy_Skeleton enemy;

    public SkeletonMoveState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, enemyData, animBoolName, skeleton)
    {
        this.enemy = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemyData.MovementVelocity * enemy.FacingDirection, enemy.Rigid.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.IdleState);
        }
    }
}
