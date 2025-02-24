using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : EnemyState
{
    private SpawnEnemyManager parentWave;
    public EnemyDieState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        parentWave = enemy.GetComponentInParent<SpawnEnemyManager>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished
        || enemy.skeletonAnimation.AnimationState.GetCurrent(0).IsComplete)
        {
            enemy.Reset();
            enemy.gameObject.SetActive(false);
        }
    }
}
