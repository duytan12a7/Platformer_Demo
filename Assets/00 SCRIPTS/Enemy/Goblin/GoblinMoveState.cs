using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoblinMoveState : GoblinGroundedState
{
    private Goblin enemy;
    private Transform playerTransform;

    public GoblinMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Goblin enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        playerTransform = GameManager.Instance.Player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        enemy.SetVelocity(enemy.MoveSpeed * enemy.FacingDirection, rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.IdleState);
        }
        else if (enemy.IsPlayerDetected())
            stateMachine.ChangeState(enemy.BattleState);
    }
}
