using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStunnedState : EnemyState
{
    private Goblin enemy;

    public GoblinStunnedState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Goblin enemy)
        : base(enemy, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.PlayAnimation("hurt", false);
        stateTimer = enemy.StunDuration;

        enemy.EntityFX.InvokeRepeating("RedColorBlink", 0, .1f);

        rb.velocity = new Vector2(-enemy.FacingDirection * enemy.StunDirection.x, enemy.StunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.EntityFX.Invoke("CancelColorChange", 0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.IdleState);
    }
}
