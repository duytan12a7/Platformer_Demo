using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStunnedState : EnemyStunnedState
{

    public GoblinStunnedState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName)
    {
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
    }
}
