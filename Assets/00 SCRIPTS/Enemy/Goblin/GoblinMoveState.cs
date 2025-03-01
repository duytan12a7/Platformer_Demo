using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMoveState : GoblinGroundedState
{
    private Goblin _enemy;
    private Transform playerTransform;

    public GoblinMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Goblin enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
    {
        _enemy = enemy;
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


        _enemy.SetVelocity(_enemy.MoveSpeed * _enemy.FacingDirection, rb.velocity.y);

        if (_enemy.IsWallDetected() || !_enemy.IsGroundDetected())
        {
            _enemy.Flip();
            stateMachine.ChangeState(_enemy.IdleState);
        }

        if (_enemy.IsPlayerDetected())
            stateMachine.ChangeState(_enemy.BattleState);
    }
}
