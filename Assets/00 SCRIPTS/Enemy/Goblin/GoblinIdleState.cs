using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinIdleState : GoblinGroundedState
{
    private Goblin _enemy;
    private Transform playerTransform;

    public GoblinIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Goblin enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        playerTransform = GameManager.Instance.Player.transform;
        stateTimer = _enemy.IdleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (stateTimer < 0)
            stateMachine.ChangeState(_enemy.MoveState);
    }
}
