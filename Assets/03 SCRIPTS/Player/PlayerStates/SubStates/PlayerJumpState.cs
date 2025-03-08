using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private int amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.AmountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(rb.velocity.x, player.JumpForce);
        amountOfJumpsLeft--;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.InAirState);
    }

    public bool CanJump() => amountOfJumpsLeft > 0;

    public void ResetAmountJumpsLeft() => amountOfJumpsLeft = playerData.AmountOfJumps;
}
