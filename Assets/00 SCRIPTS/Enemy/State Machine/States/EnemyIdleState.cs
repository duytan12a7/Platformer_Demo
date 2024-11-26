using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.CheckAggroDistance() || enemy.CheckAggroRadius())
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        else if (stateTimer < 0)
            stateMachine.ChangeState(enemy.WanderState);
    }
}
