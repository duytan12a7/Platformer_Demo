using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyState
{
    private float hitDirection;

    public EnemyHurtState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (Mathf.Sign(enemy.FacingDirection) != hitDirection)
            enemy.Flip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished 
        || enemy.skeletonAnimation.AnimationState.GetCurrent(0).IsComplete)
            enemy.StateMachine.ChangeState(enemy.ChaseState);
    }

    public void SetHitDirection(float direction) => hitDirection = direction;

}
