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

        if (Input.GetKeyDown(KeyCode.C))
            stateMachine.ChangeState(player.AttackState);

        if (Input.GetKeyDown(KeyCode.X) && player.JumpState.CanJump())
            stateMachine.ChangeState(player.JumpState);

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
