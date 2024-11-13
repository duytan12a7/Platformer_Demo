using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region StateMachine Variables

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }

    #endregion

    #region Components

    public Animator Anim { get; private set; }
    public Rigidbody2D Rigid { get; private set; }
    [SerializeField] private PlayerData playerData;

    #endregion

    #region Check Transform
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    #endregion

    #region Other Variables

    public Vector2 CurrentVelocity;
    private Vector2 workspace;
    public int FacingDirection;

    #endregion
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, Global.AnimatorParams.Idle);
        MoveState = new PlayerMoveState(this, StateMachine, playerData, Global.AnimatorParams.Move);
        JumpState = new PlayerJumpState(this, StateMachine, playerData, Global.AnimatorParams.InAir);
        InAirState = new PlayerInAirState(this, StateMachine, playerData, Global.AnimatorParams.InAir);
        LandState = new PlayerLandState(this, StateMachine, playerData, Global.AnimatorParams.Land);
        DashState = new PlayerDashState(this, StateMachine, playerData, Global.AnimatorParams.Move);
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, Global.AnimatorParams.WallSlide);
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, Global.AnimatorParams.InAir);
        AttackState = new PlayerAttackState(this, StateMachine, playerData, Global.AnimatorParams.Attack);
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        Rigid = GetComponent<Rigidbody2D>();

        FacingDirection = 1;

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = Rigid.velocity;
        StateMachine.CurrentState.Update();

        CheckIfDashInput();
    }

    #region Set Functions

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        Rigid.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.y, velocity);
        Rigid.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rigid.velocity = new Vector2(xVelocity, yVelocity);
    }

    #endregion

    #region Check Functions

    public bool IsGroundDetected() => Physics2D.OverlapCircle(groundCheck.position, playerData.GroundCheckRadius, playerData.WhatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.WallCheckDistance, playerData.WhatIsGround);

    public void CheckFlip(float xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
            Flip();
    }

    private void CheckIfDashInput()
    {
        if (Input.GetKeyDown(KeyCode.Z) && DashState.CanDash())
            StateMachine.ChangeState(DashState);
    }

    #endregion

    #region Other Functions

    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    #endregion
}
