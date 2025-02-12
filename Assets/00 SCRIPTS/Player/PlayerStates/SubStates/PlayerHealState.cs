using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealState : PlayerState
{
    public PlayerHealState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
        Debug.Log("Player Heal State");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
            stateMachine.ChangeState(player.IdleState);
    }
}
