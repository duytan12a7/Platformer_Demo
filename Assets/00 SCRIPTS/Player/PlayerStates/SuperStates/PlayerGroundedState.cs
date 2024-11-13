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
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.C))
            stateMachine.ChangeState(player.AttackState);

        if (Input.GetKeyDown(KeyCode.X) && player.IsGroundDetected())
            stateMachine.ChangeState(player.JumpState);
        else if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.InAirState);

    }
}
