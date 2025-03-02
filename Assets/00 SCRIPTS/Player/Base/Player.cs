using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Player : Entity
{
    #region StateMachine Variables

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; set; }
    public PlayerMoveState MoveState { get; set; }
    public PlayerLandState LandState { get; set; }
    public PlayerDashState DashState { get; set; }
    public PlayerJumpState JumpState { get; set; }
    public PlayerInAirState InAirState { get; set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerAttackState AttackState { get; set; }
    public PlayerDieState DeadState { get; set; }
    public PlayerHealState HealState { get; private set; }
    public PlayerCounterAttackState CounterAttackState { get; private set; }

    #endregion

    #region Components

    protected ICharacterAnimation characterAnimation;
    public SkeletonAnimation skeletonAnimation;
    public Animator animator;

    public PlayerData playerData;
    public PlayerStats Stats { get; private set; }

    #endregion

    #region Unity Callback Functions

    protected override void Awake()
    {
        base.Awake();

        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
        animator = GetComponentInChildren<Animator>();

        if (skeletonAnimation != null)
            characterAnimation = new SpineCharacterAnimation(skeletonAnimation);
        else if (animator != null)
            characterAnimation = new AnimatorCharacterAnimation(animator);
            
        StateMachine = new PlayerStateMachine();
        InitializeStateMachine();
    }

    protected virtual void InitializeStateMachine()
    {
        IdleState = new PlayerIdleState(this, StateMachine, playerData, Global.AnimatorParams.Idle);
        MoveState = new PlayerMoveState(this, StateMachine, playerData, Global.AnimatorParams.Move);
        JumpState = new PlayerJumpState(this, StateMachine, playerData, Global.AnimatorParams.InAir);
        InAirState = new PlayerInAirState(this, StateMachine, playerData, Global.AnimatorParams.InAir);
        LandState = new PlayerLandState(this, StateMachine, playerData, Global.AnimatorParams.Land);
        DashState = new PlayerDashState(this, StateMachine, playerData, Global.AnimatorParams.Dash);
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, Global.AnimatorParams.WallSlide);
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, Global.AnimatorParams.InAir);
        AttackState = new PlayerAttackState(this, StateMachine, playerData, Global.AnimatorParams.Attack);
        DeadState = new PlayerDieState(this, StateMachine, playerData, Global.AnimatorParams.Die);
        HealState = new PlayerHealState(this, StateMachine, playerData, Global.AnimatorParams.Heal);
        CounterAttackState = new PlayerCounterAttackState(this, StateMachine, playerData, Global.AnimatorParams.CounterAttack);
    }

    protected override void Start()
    {
        base.Start();
        DefaultFacing();

        Stats = GetComponentInChildren<PlayerStats>();
        StateMachine.Initialize(IdleState);
        UpdateCurrentStats();
    }

    private void UpdateCurrentStats()
    {
        Stats.UpdateMovementSpeed(playerData.MovementVelocity);
    }

    protected override void Update()
    {
        base.Update();
        CurrentVelocity = Rigid.velocity;
        StateMachine.CurrentState.LogicUpdate();

        CheckIfDashInput();
    }

    #endregion

    #region Check Functions

    private void CheckIfDashInput()
    {
        if (Input.GetKeyDown(KeyCode.Z) && DashState.CanDash())
            StateMachine.ChangeState(DashState);
    }

    public bool IsDashing() => DashState.IsDashing;

    public void DownJump()
    {
        Collider2D playerCollider = GetComponent<Collider2D>();

        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, whatIsGround);

        if (rayHit.collider.GetComponent<PlatformEffector2D>())
            StartCoroutine(IEDownJump(playerCollider, rayHit.collider.GetComponent<CompositeCollider2D>()));
    }

    IEnumerator IEDownJump(Collider2D playerCollider, Collider2D collider)
    {
        Physics2D.IgnoreCollision(playerCollider, collider);
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreCollision(playerCollider, collider, false);
    }


    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();



    public override void Die()
    {
        base.Die();

        StateMachine.ChangeState(DeadState);
    }

    #endregion

    #region Skeleton Animation

    public virtual void PlayAnimation(string animationName, bool loop = false)
    {
        if (characterAnimation == null) return;
        characterAnimation.PlayAnimation(animationName, loop);
    }

    public virtual void SetSpeedAnimation(float speed)
    {
        if (characterAnimation == null) return;
        characterAnimation.SetSpeedAnimation(speed);
    }

    public virtual void SetTrigger(string triggerName)
    {
        if (characterAnimation == null) return;
        characterAnimation.SetTrigger(triggerName);
    }

    public virtual void StopAnimation(string animationName, bool loop)
    {
        if (characterAnimation == null) return;
        characterAnimation.StopAnimation(animationName, loop);
    }

    #endregion
}
