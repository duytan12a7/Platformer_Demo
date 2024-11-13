using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.C))
            stateMachine.ChangeState(player.AttackState);

        if (player.IsWallDetected())
            stateMachine.ChangeState(player.WallSlideState);

        if (rb.velocity.y == 0)
            stateMachine.ChangeState(player.LandState);

        if (xInput != 0)
        {
            player.CheckFlip(xInput);
            player.SetVelocity(playerData.MovementVelocity * xInput, rb.velocity.y);
        }
    }
}
