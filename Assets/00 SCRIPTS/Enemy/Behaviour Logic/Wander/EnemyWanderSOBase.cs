using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWanderSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    public float AggroCheckDistance;
    public float AggroCheckRadius;

    public virtual void Initialize(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        playerTransform = GameManager.Instance.Player.transform;
    }

    public virtual void Enter() { }
    public virtual void Exit()
    {
        ResetValues();
    }
    public virtual void LogicUpdate()
    {
        if (enemy.CheckAggroDistance() || enemy.CheckAggroRadius())
            enemy.StateMachine.ChangeState(enemy.ChaseState);
    }
    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
