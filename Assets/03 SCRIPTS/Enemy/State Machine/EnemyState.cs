using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;

    protected Rigidbody2D rb;

    protected bool isAnimationFinished;

    protected float stateTimer;

    protected string animBoolName;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        rb = enemy.Rigid;
        enemy.PlayAnimation(animBoolName, true);
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {
        enemy.StopAnimation(animBoolName, false);
        enemy.AssignLastAnimName(animBoolName);
    }

    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
