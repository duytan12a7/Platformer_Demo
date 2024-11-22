using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWanderState : EnemyState
{

    public EnemyWanderState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.EnemyWanderBaseInstance.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.EnemyWanderBaseInstance.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.EnemyWanderBaseInstance.LogicUpdate();
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        enemy.EnemyWanderBaseInstance.AnimationTriggerEvent(triggerType);
    }
}
