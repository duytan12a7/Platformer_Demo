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

        // if (Input.GetKeyDown(KeyCode.Q))
        //     stateMachine.ChangeState(player.CounterAttackState);

        if (player.InputHandler.AttackInput)
            stateMachine.ChangeState(player.AttackState);

        if (player.InputHandler.JumpInput && yInput < 0)
        {
            player.InputHandler.UseJumpInput();
            player.DownJump();
        }
        else if (player.InputHandler.JumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.InAirState);

    }
}
