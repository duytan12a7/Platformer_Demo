using System;
using Spine.Unity;
using UnityEngine;

public class Enemy : Entity
{
    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyMoveState MoveState { get; set; }
    public EnemyBattleState BattleState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyStunnedState StunnedState { get; set; }
    public EnemyDeadState DeadState { get; set; }

    #endregion

    #region Components
    public EnemyStats Stats { get; private set; }

    #endregion

    #region Other Variables
    public AnimationTriggerType CurrentTriggerType { get; private set; }

    [Header("Stunned info")]
    public float StunDuration = .35f;
    public Vector2 StunDirection = new(2, 1);
    protected bool canBeStunned;
    public bool IsStunned = true;
    [SerializeField] protected GameObject counterImage;

    [Header("Move info")]
    public float MoveSpeed = 2f;
    public float IdleTime = 1f;
    public float BattleTime = 7f;
    private float defaultMoveSpeed;

    [Header("Attack info")]
    public float AgroDistance = 2;
    public float AttackDistance = 1.5f;
    public float AttackCheckDistance = 5f;

    [Header(" Dash info")]
    public float DashSpeed = 20f;
    public float DashDuration = 5f;
    private float defaultDashSpeed;

    [Header(" Skill info")]
    public bool IsSkillAttackActive = true;

    public string LastAnimBoolName { get; private set; }

    #endregion

    #region Unity Callback Functions

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();

        Stats = GetComponentInChildren<EnemyStats>();
        defaultMoveSpeed = MoveSpeed;
        defaultDashSpeed = DashSpeed;
    }

    protected override void Update()
    {
        base.Update();

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

    public virtual RaycastHit2D IsPlayerDetected()
    {
        RaycastHit2D bottomDetected = Physics2D.Raycast(groundCheck.position, Vector2.right * FacingDirection, AttackCheckDistance, whatIsCharacter);
        RaycastHit2D rightDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, AttackCheckDistance, whatIsCharacter);
        RaycastHit2D leftDetected = Physics2D.Raycast(wallCheck.position, Vector2.left * FacingDirection, AttackCheckDistance, whatIsCharacter);
        RaycastHit2D wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, AttackCheckDistance, whatIsGround);

        if (bottomDetected)
            return bottomDetected;

        if (leftDetected)
            return leftDetected;

        if (wallDetected)
        {
            if (wallDetected.distance < rightDetected.distance)
                return default(RaycastHit2D);
        }

        return rightDetected;
    }

    public bool CheckAttackDistance()
    {
        RaycastHit2D bottomDetected = Physics2D.Raycast(groundCheck.position, Vector2.right * FacingDirection, AttackDistance, whatIsCharacter);
        RaycastHit2D attackDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, AttackDistance, whatIsCharacter);

        if (bottomDetected)
            return bottomDetected;
        return attackDetected;
    }

    #endregion

    #region Animation Trigger Functions

    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        CurrentTriggerType = triggerType;
        StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
    }

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public virtual void AnimationSpecialAttackTrigger()
    {

    }

    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    public enum AnimationTriggerType
    {
        None,
        EffectAttack,
        EnemyDamaged,
        PlayerFootstepsound
    }

    #endregion

    #region Other Functions

    public override void DamageEffect(Transform attacker)
    {
        base.DamageEffect(attacker);
        if (IsStunned)
            StateMachine.ChangeState(StunnedState);
    }

    public virtual void OpenSkillAttack() => IsSkillAttackActive = true;
    public virtual void CloseSkillAttack() => IsSkillAttackActive = false;

    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();
        StateMachine.ChangeState(DeadState);
    }

    public virtual void AssignLastAnimName(string _animBoolName) => LastAnimBoolName = _animBoolName;

    public override void SlowEntityBy(float slowPercentage, float slowDuration)
    {
        MoveSpeed *= (1 - slowPercentage);
        DashSpeed *= (1 - slowPercentage);
        SetSpeedAnimation(1 - slowPercentage);

        Invoke(nameof(ReturnDefaultSpeed), slowDuration);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();
        MoveSpeed = defaultMoveSpeed;
        DashSpeed = defaultDashSpeed;
    }

    public virtual void Reset()
    {
        StateMachine.ChangeState(IdleState);
        DefaultFacing();
        Stats.Reset();
        CloseCounterAttackWindow();

        SetSpeedAnimation(1f);
        BoxCollider.enabled = true;
    }

    #endregion

    protected override void OnDrawGizmos()
    {
        // base.OnDrawGizmos();
        // Gizmos.color = Color.red;
        // Gizmos.DrawRay(wallCheck.position, Vector2.right * FacingDirection * AttackCheckDistance);
        // Gizmos.DrawRay(wallCheck.position, Vector2.left * FacingDirection * AttackCheckDistance);
        // Gizmos.color = Color.green;
        // Gizmos.DrawRay(wallCheck.position, Vector2.right * FacingDirection * AttackDistance);
    }
}
