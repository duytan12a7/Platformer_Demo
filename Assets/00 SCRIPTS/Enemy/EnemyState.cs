using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;
    protected EnemyData enemyData;

    protected Enemy_Skeleton skeleton;

    protected float stateTimer;
    protected bool isAnimationFinished;
    private string animBoolName;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, Enemy_Skeleton skeleton)
    {
        this.enemyBase = enemy;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
        this.animBoolName = animBoolName;
        this.skeleton = skeleton;
    }

    public virtual void Enter()
    {
        enemyBase.Anim.SetBool(animBoolName, true);
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {
        enemyBase.Anim.SetBool(animBoolName, false);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void AnimationTrigger() { }
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
