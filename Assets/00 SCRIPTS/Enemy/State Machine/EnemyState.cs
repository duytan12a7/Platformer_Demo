using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;

    public EnemyState(Enemy emeny, EnemyStateMachine stateMachine)
    {
        this.enemy = emeny;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }

    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) { }
}
