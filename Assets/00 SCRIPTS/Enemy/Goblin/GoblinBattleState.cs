using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBattleState : EnemyState
{
    private readonly Goblin _enemy;
    private Transform playerTransform;

    public GoblinBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Goblin enemy)
        : base(enemy, stateMachine, animBoolName)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        playerTransform = GameManager.Instance.Player.transform;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_enemy.IsPlayerDetected())
        {
            stateTimer = _enemy.BattleTime;
            if (_enemy.CheckAttackDistance())
                _enemy.StateMachine.ChangeState(_enemy.AttackState);
        }
        else if (stateTimer < 0 || Vector2.Distance(playerTransform.position, _enemy.transform.position) > 7)
            stateMachine.ChangeState(_enemy.IdleState);

        float directionX = (playerTransform.position.x - _enemy.transform.position.x) > 0 ? 1 : -1;
        _enemy.SetVelocityX(directionX * _enemy.MoveSpeed);
        _enemy.CheckFlip(directionX);
    }
}
