using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{

    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.EnemyAttackBaseInstance.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.EnemyAttackBaseInstance.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.EnemyAttackBaseInstance.Update();
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        enemy.EnemyAttackBaseInstance.AnimationTriggerEvent(triggerType);
    }
}
