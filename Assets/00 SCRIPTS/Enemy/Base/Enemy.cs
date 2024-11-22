using System;
using UnityEngine;

public class Enemy : Entity
{
    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyWanderState WanderState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyHurtState HurtState { get; private set; }
    public EnemyDieState DieState { get; private set; }

    #endregion

    #region ScriptableObject Variables

    [SerializeField] private EnemyWanderSOBase _enemyWanderBase;
    [SerializeField] private EnemyChaseSOBase _enemyChaseBase;
    [SerializeField] private EnemyAttackSOBase _enemyAttackBase;

    public EnemyWanderSOBase EnemyWanderBaseInstance { get; private set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; private set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; private set; }

    #endregion

    #region Components

    public EnemyStats Stats { get; private set; }

    #endregion

    #region Other Variables
    public AnimationTriggerType CurrentTriggerType { get; private set; }

    #endregion

    public Action OnFlipped;

    #region Unity Callback Functions

    protected override void Awake()
    {
        base.Awake();

        InitializeEnemyInstances();
        InitializeStateMachine();
    }

    private void InitializeEnemyInstances()
    {
        EnemyWanderBaseInstance = Instantiate(_enemyWanderBase);
        EnemyChaseBaseInstance = Instantiate(_enemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(_enemyAttackBase);
    }

    private void InitializeStateMachine()
    {
        StateMachine = new EnemyStateMachine();

        WanderState = new EnemyWanderState(this, StateMachine, Global.AnimatorParams.Move);
        ChaseState = new EnemyChaseState(this, StateMachine, Global.AnimatorParams.Move);
        AttackState = new EnemyAttackState(this, StateMachine, Global.AnimatorParams.Attack);
        HurtState = new EnemyHurtState(this, StateMachine, Global.AnimatorParams.Hurt);
        DieState = new EnemyDieState(this, StateMachine, Global.AnimatorParams.Die);
    }

    protected override void Start()
    {
        base.Start();

        Stats = GetComponentInChildren<EnemyStats>();
        InitializeBase();
    }

    private void InitializeBase()
    {
        EnemyWanderBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        StateMachine.Initialize(WanderState);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    #endregion
    
    #region Check Functions

    public override void CheckFlip(float xInput)
    {
        if (isKnocked) return;

        if (xInput < 0f && isFacingRight || xInput > 0f && !isFacingRight)
            Flip();
    }

    public override void Flip()
    {
        base.Flip();
        OnFlipped();
    }

    public bool CheckAggroDistance() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, _enemyWanderBase.AggroCheckDistance, whatIsCharacter);

    public bool CheckAttackDistance() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, _enemyChaseBase.AttackCheckDistance, whatIsCharacter);

    public bool CheckAggroRadius() => Physics2D.OverlapCircle(wallCheck.position, _enemyWanderBase.AggroCheckRadius, whatIsCharacter);

    public bool CheckAttackRadius() => Physics2D.OverlapCircle(wallCheck.position, _enemyChaseBase.AttackCheckRadius, whatIsCharacter);

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

    // protected override void OnDrawGizmos()
    // {
    //     base.OnDrawGizmos();
    //     Gizmos.color = Color.yellow;
    //     // Gizmos.DrawRay(wallCheck.position, Vector2.right * FacingDirection * _enemyIdleBase.AggroCheckDistance);
    //     Gizmos.DrawWireSphere(wallCheck.position, _enemyIdleBase.AggroCheckRadius);
    //     Gizmos.color = Color.blue;
    //     // Gizmos.DrawRay(wallCheck.position, Vector2.right * FacingDirection * _enemyChaseBase.AttackCheckDistance);
    //     Gizmos.DrawWireSphere(wallCheck.position, _enemyChaseBase.AttackCheckRadius);
    // }

    #endregion
}
