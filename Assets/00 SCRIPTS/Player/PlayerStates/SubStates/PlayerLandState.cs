using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0f);
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0)
            stateMachine.ChangeState(player.MoveState);
        else if (isAnimationFinished)
            stateMachine.ChangeState(player.IdleState);
    }
}
