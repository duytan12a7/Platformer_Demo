using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Input.GetKeyDown(KeyCode.C))
            stateMachine.ChangeState(player.AttackState);

        if (Input.GetKeyDown(KeyCode.X) && player.JumpState.CanJump())
            stateMachine.ChangeState(player.JumpState);
        else if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.InAirState);

    }
}
