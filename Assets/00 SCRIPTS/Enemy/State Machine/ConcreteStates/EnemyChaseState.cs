using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.EnemyChaseBaseInstance.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.EnemyChaseBaseInstance.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.EnemyChaseBaseInstance.Update();
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        enemy.EnemyChaseBaseInstance.AnimationTriggerEvent(triggerType);
    }
}
