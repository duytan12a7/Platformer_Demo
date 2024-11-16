using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;

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
    }
    public virtual void Exit()
    {
        enemy.Anim.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate() { }

    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) { }
}
