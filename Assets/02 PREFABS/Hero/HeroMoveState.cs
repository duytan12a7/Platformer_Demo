using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveState : PlayerMoveState
{
    public HeroMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;
        xInput = (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) - (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0);
        yInput = (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) - (Input.GetKey(KeyCode.DownArrow) ? 1 : 0);
        
        player.CheckFlip(xInput);
        player.SetVelocity(xInput * playerData.MovementVelocity, rb.velocity.y);

        if (xInput == 0)
            stateMachine.ChangeState(player.IdleState);
    }
}
