using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Input.GetKeyDown(KeyCode.X))
        {
            stateMachine.ChangeState(player.WallJumpState);
            player.Flip();
            return;
        }

        if (xInput != 0 && xInput != player.FacingDirection)
            stateMachine.ChangeState(player.IdleState);

        if (yInput < 0)
            player.SetVelocity(rb.velocity.x, rb.velocity.y);
        else
            player.SetVelocity(rb.velocity.x, rb.velocity.y * playerData.WallSlideVelocity);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);
    }
}
