using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
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
    public PlayerDieState DieState { get; private set; }
    public PlayerHealState HealState { get; private set; }

    #endregion

    #region Components

    [SerializeField] private PlayerData playerData;
    public PlayerStats Stats { get; private set; }

    #endregion

    #region Unity Callback Functions

    protected override void Awake()
    {
        base.Awake();
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, Global.AnimatorParams.Idle);
        MoveState = new PlayerMoveState(this, StateMachine, playerData, Global.AnimatorParams.Move);
        JumpState = new PlayerJumpState(this, StateMachine, playerData, Global.AnimatorParams.InAir);
        InAirState = new PlayerInAirState(this, StateMachine, playerData, Global.AnimatorParams.InAir);
        LandState = new PlayerLandState(this, StateMachine, playerData, Global.AnimatorParams.Land);
        DashState = new PlayerDashState(this, StateMachine, playerData, Global.AnimatorParams.Dash);
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, Global.AnimatorParams.WallSlide);
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, Global.AnimatorParams.InAir);
        AttackState = new PlayerAttackState(this, StateMachine, playerData, Global.AnimatorParams.Attack);
        DieState = new PlayerDieState(this, StateMachine, playerData, Global.AnimatorParams.Die);
        HealState = new PlayerHealState(this, StateMachine, playerData, Global.AnimatorParams.Heal);
    }

    protected override void Start()
    {
        base.Start();
        DefaultFacing();

        Stats = GetComponentInChildren<PlayerStats>();
        StateMachine.Initialize(IdleState);
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

    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    #endregion
}
