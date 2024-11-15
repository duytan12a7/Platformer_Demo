using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{

    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.EnemyIdleBaseInstance.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.EnemyIdleBaseInstance.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.EnemyIdleBaseInstance.Update();
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        enemy.EnemyIdleBaseInstance.AnimationTriggerEvent(triggerType);
    }
}
