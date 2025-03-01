using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStunnedState : EnemyState
{
    private readonly Goblin _enemy;

    public GoblinStunnedState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Goblin enemy)
        : base(enemy, stateMachine, animBoolName)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.PlayAnimation("hurt", false);
        stateTimer = _enemy.StunDuration;

        _enemy.EntityFX.InvokeRepeating("RedColorBlink", 0, .1f);

        rb.velocity = new Vector2(-_enemy.FacingDirection * _enemy.StunDirection.x, _enemy.StunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();

        _enemy.EntityFX.Invoke("CancelColorChange", 0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateTimer < 0)
            stateMachine.ChangeState(_enemy.IdleState);
    }
}
