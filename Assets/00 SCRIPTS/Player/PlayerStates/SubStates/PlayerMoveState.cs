using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckFlip(xInput);
        player.SetVelocity(xInput * playerData.MovementVelocity, rb.velocity.y);

        if (xInput == 0)
            stateMachine.ChangeState(player.IdleState);
        // else if (Input.GetKeyDown(KeyCode.A))
        //     stateMachine.ChangeState(player.HealState);
    }
}
