using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;

    protected Rigidbody2D rb;

    protected bool isAnimationFinished;

    protected float stateTimer;

    private string animBoolName;

    public EnemyState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.enemyBase = enemyBase;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        rb = enemyBase.Rigid;
        enemyBase.PlayAnimation(animBoolName, true);
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {
        enemyBase.StopAnimation();
        enemyBase.AssignLastAnimName(animBoolName);
    }

    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
