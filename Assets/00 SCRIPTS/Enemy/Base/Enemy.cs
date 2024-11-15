using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
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

    #region Check Surroundings
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float _groundCheckRadius = 0.1f;
    [SerializeField] private float _wallCheckDistance = 0.5f;
    #endregion

    #region Components

    public Rigidbody2D Rigid { get; set; }
    public Animator Anim { get; set; }

    #endregion

    #region Other Variables

    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    public int FacingDirection { get; set; }
    public bool IsFacingRight { get; set; }

    public bool IsAggroed { get; set; }
    public bool IsWithinAttackDistance { get; set; }
    public AnimationTriggerType CurrentTriggerType { get; private set; }

    #endregion

    #region Unity Callback Functions

    private void Awake()
    {

        EnemyIdleBaseInstance = Instantiate(_enemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(_enemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(_enemyAttackBase);

        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

        Rigid = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        FacingDirection = 1;
        IsFacingRight = true;

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }

    #endregion

    #region Health / Die Functions

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0f)
            Die();
    }

    public void Die()
    {

    }

    #endregion


    #region Movement Functions

    public void MoveEnemy(Vector2 velocity)
    {
        Rigid.velocity = velocity;
    }

    public void SetVelocityX(float velocity)
    {
        Rigid.velocity = new Vector2(velocity, Rigid.velocity.y);
    }

    public virtual void CheckFlip(float xInput)
    {
        if (xInput < 0f && IsFacingRight || xInput > 0f && !IsFacingRight)
            Flip();
    }

    #endregion


    #region Check Functions

    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetAttackDistanceBool(bool isWithinAttackDistance)
    {
        IsWithinAttackDistance = isWithinAttackDistance;
    }

    public bool IsGroundDetected() => Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround);

    public bool IsWallDetected() => Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _wallCheckDistance, _whatIsGround);

    public virtual void Flip()
    {
        FacingDirection *= -1;
        IsFacingRight = !IsFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        Gizmos.DrawRay(_wallCheck.position, Vector2.right * FacingDirection * _wallCheckDistance);
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
