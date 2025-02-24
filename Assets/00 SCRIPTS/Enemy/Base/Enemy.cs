using System;
using Spine.Unity;
using UnityEngine;
using Spine;

public class Enemy : Entity
{
    protected ICharacterAnimation characterAnimation;
    public SkeletonAnimation skeletonAnimation;
    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
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

        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();

        if (skeletonAnimation != null)
            characterAnimation = new SpineAdapter(skeletonAnimation);
        else if (Anim != null)
            characterAnimation = new AnimatorAdapter(Anim);
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

        IdleState = new EnemyIdleState(this, StateMachine, "idle");
        WanderState = new EnemyWanderState(this, StateMachine, "move");
        ChaseState = new EnemyChaseState(this, StateMachine, "move");
        AttackState = new EnemyAttackState(this, StateMachine, "skill_0");
        HurtState = new EnemyHurtState(this, StateMachine, "hurt");
        DieState = new EnemyDieState(this, StateMachine, "die");
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

        StateMachine.Initialize(IdleState);
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

    public virtual void Reset()
    {
        StateMachine.Initialize(IdleState);
        DefaultFacing();
        Stats.Reset();
        entityFX.Reset();
    }

    public void EnterHurtState()
    {
        Vector2 playerPosition = GameManager.Instance.Player.transform.position;
        float direction = Mathf.Sign(transform.position.x - playerPosition.x);
        HurtState.SetHitDirection(direction);

        StateMachine.ChangeState(HurtState);
    }

    #endregion

    public void PlayAnimation(string animationName, bool loop = false)
    {
        if (characterAnimation == null)
        {
            Debug.LogError($"[Enemy] characterAnimation is NULL! Không thể chơi animation {animationName}");
            return;
        }

        characterAnimation.PlayAnimation(animationName, loop);
    }

    public virtual void SetTrigger(string triggerName)
    {
        characterAnimation.SetTrigger(triggerName);
    }

    public virtual void StopAnimation()
    {
        characterAnimation.StopAnimation();
    }
}
