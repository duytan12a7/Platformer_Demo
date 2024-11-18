using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, IDamageable, ITriggerCheckable
{
    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }

    #endregion

    #region ScriptableObject Variables

    [SerializeField] private EnemyIdleSOBase _enemyIdleBase;
    [SerializeField] private EnemyChaseSOBase _enemyChaseBase;
    [SerializeField] private EnemyAttackSOBase _enemyAttackBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }

    #endregion

    #region Other Variables

    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }

    public bool IsAggroed { get; set; }
    public bool IsWithinAttackDistance { get; set; }
    public AnimationTriggerType CurrentTriggerType { get; private set; }

    #endregion

    #region Unity Callback Functions

    protected override void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(_enemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(_enemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(_enemyAttackBase);

        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }

    protected override void Start()
    {
        base.Start();
        CurrentHealth = MaxHealth;

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.Update();
    }

    #endregion

    #region Health / Die Functions

    public virtual void Damage(float damageAmount)
    {
        Debug.Log(gameObject.name + " was damaged");
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0f)
            Die();
    }

    public virtual void Die()
    {
    }

    #endregion

    #region Check Functions

    public override void CheckFlip(float xInput)
    {
        if (xInput < 0f && isFacingRight || xInput > 0f && !isFacingRight)
            Flip();
    }

    #endregion

    #region Set Functions

    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetAttackDistanceBool(bool isWithinAttackDistance)
    {
        IsWithinAttackDistance = isWithinAttackDistance;
    }

    public void SetVelocity(Vector2 velocity)
    {
        Rigid.velocity = velocity;
    }

    #endregion

    #region Animation Trigger Functions

    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        CurrentTriggerType = triggerType;
        StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        None,
        EffectAttack,
        EnemyDamaged,
        PlayerFootstepsound
    }

    #endregion
}
