using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyHurtState HurtState { get; private set; }

    #endregion

    #region ScriptableObject Variables

    [SerializeField] private EnemyIdleSOBase _enemyIdleBase;
    [SerializeField] private EnemyChaseSOBase _enemyChaseBase;
    [SerializeField] private EnemyAttackSOBase _enemyAttackBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; private set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; private set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; private set; }

    #endregion

    #region Other Variables
    public AnimationTriggerType CurrentTriggerType { get; private set; }

    #endregion

    #region Unity Callback Functions

    protected override void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(_enemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(_enemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(_enemyAttackBase);

        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine, "Move");
        ChaseState = new EnemyChaseState(this, StateMachine, "Move");
        AttackState = new EnemyAttackState(this, StateMachine, "Attack");
        HurtState = new EnemyHurtState(this, StateMachine, "Hurt");
    }

    protected override void Start()
    {
        base.Start();

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    #endregion

    #region Health / Die Functions

    public override void Damage(float damageAmount)
    {
        StartCoroutine(HitKnockback());

        entityFX.StartCoroutine(entityFX.HitFlashFX());

        StateMachine.ChangeState(HurtState);

        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0f)
            Die();
    }

    public override void Die()
    {
    }

    #endregion

    #region Check Functions

    public override void CheckFlip(float xInput)
    {
        if (isKnocked) return;

        if (xInput < 0f && isFacingRight || xInput > 0f && !isFacingRight)
            Flip();
    }

    public bool CheckAggroDistance() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, _enemyIdleBase.AggroCheckDistance, whatIsCharacter);

    public bool CheckAttackDistance() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, _enemyChaseBase.AttackCheckDistance, whatIsCharacter);

    public bool CheckAggroRadius() => Physics2D.OverlapCircle(wallCheck.position, _enemyIdleBase.AggroCheckRadius, whatIsCharacter);

    public bool CheckAttackRadius() => Physics2D.OverlapCircle(wallCheck.position, _enemyChaseBase.AttackCheckRadius, whatIsCharacter);

    #endregion

    #region Set Functions

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

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public enum AnimationTriggerType
    {
        None,
        EffectAttack,
        EnemyDamaged,
        PlayerFootstepsound
    }

    #endregion

    #region Other Functions

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawRay(wallCheck.position, Vector2.right * FacingDirection * _enemyIdleBase.AggroCheckDistance);
        // Gizmos.DrawWireSphere(wallCheck.position, _enemyIdleBase.AggroCheckRadius);
        // Gizmos.color = Color.blue;
        // Gizmos.DrawRay(wallCheck.position, Vector2.right * FacingDirection * _enemyChaseBase.AttackCheckDistance);
        // Gizmos.DrawWireSphere(wallCheck.position, _enemyChaseBase.AttackCheckRadius);
    }

    #endregion
}
