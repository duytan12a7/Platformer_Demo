using UnityEngine;

public class EnemyChaseSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    protected float stateTimer;

    public float AttackCheckDistance;
    public float AttackCheckRadius;

    public float movementSpeed;

    public virtual void Initialize(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        playerTransform = GameManager.Instance.Player.transform;
    }

    public virtual void Enter()
    {
        enemy.Stats.UpdateMovementSpeed(movementSpeed);
    }
    public virtual void Exit()
    {
        ResetValues();
    }
    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;

        if (enemy.CheckAttackDistance() || enemy.CheckAttackRadius())
            enemy.StateMachine.ChangeState(enemy.AttackState);
    }

    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) { }

    public virtual void ResetValues() { }
}
