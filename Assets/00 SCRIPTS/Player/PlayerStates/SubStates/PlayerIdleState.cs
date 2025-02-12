using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0f, 0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xInput != 0)
            stateMachine.ChangeState(player.MoveState);
        else if (Input.GetKeyDown(KeyCode.A))
            stateMachine.ChangeState(player.HealState);
    }
}
