using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    protected float stateTimer;

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
        stateTimer -= Time.deltaTime;
    }
    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) { }
    public virtual void ResetValues()
    {
        enemy.AnimationTriggerEvent(Enemy.AnimationTriggerType.None);
    }
}
