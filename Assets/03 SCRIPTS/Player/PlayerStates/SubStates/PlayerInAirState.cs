using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.InputHandler.AttackInput)
            stateMachine.ChangeState(player.AttackState);

        if (player.InputHandler.JumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
            player.InputHandler.UseJumpInput();
        }

        // if (player.IsWallDetected() && rb.velocity.y < 0.01f)
        //     stateMachine.ChangeState(player.WallSlideState);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.LandState);

        if (xInput != 0)
        {
            player.CheckFlip(xInput);
            player.SetVelocity(player.MoveSpeed * xInput, rb.velocity.y);
        }
    }
}
