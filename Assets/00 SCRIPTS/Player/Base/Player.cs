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
    public PlayerDeadState DeadState { get; set; }
    public PlayerHealState HealState { get; private set; }
    public PlayerCounterAttackState CounterAttackState { get; private set; }

    #endregion

    [Header(" Move info ")]
    public float MoveSpeed = 12f;
    public float JumpForce;
    private float defaultMoveSpeed;
    private float defaultJumpForce;
    [Header(" Dash info ")]
    public float DashSpeed;
    private float defaultDashSpeed;

    #region Components

    public PlayerData playerData;
    public PlayerStats Stats { get; private set; }

    #endregion

    #region Unity Callback Functions

    protected override void Awake()
    {
        base.Awake();
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
        DeadState = new PlayerDeadState(this, StateMachine, playerData, Global.AnimatorParams.Die);
        HealState = new PlayerHealState(this, StateMachine, playerData, Global.AnimatorParams.Heal);
        CounterAttackState = new PlayerCounterAttackState(this, StateMachine, playerData, Global.AnimatorParams.CounterAttack);
    }

    protected override void Start()
    {
        base.Start();
        DefaultFacing();

        Stats = GetComponentInChildren<PlayerStats>();
        StateMachine.Initialize(IdleState);

        defaultMoveSpeed = MoveSpeed;
        defaultJumpForce = JumpForce;
        defaultDashSpeed = DashSpeed;
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

    public override void SlowEntityBy(float slowPercentage, float slowDuration)
    {
        MoveSpeed *= (1 - slowPercentage);
        JumpForce *= (1 - slowPercentage);
        DashSpeed *= (1 - slowPercentage);
        SetSpeedAnimation(1 - slowPercentage);

        Invoke(nameof(ReturnDefaultSpeed), slowDuration);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();
        MoveSpeed = defaultMoveSpeed;
        JumpForce = defaultJumpForce;
        DashSpeed = defaultDashSpeed;
    }

    public override void Die()
    {
        base.Die();

        StateMachine.ChangeState(DeadState);
    }

    #endregion
}
