using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;

    protected bool isAnimationFinished;

    protected float stateTimer;

    private string animBoolName;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        enemy.Anim.SetBool(animBoolName, true);
        isAnimationFinished = false;
    }
    public virtual void Exit()
    {
        enemy.Anim.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
