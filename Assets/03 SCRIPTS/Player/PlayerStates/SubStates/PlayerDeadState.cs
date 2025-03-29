using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlaySFX(10);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetZeroVelocity();

        if (isAnimationFinished)
            player.Stats.CallOnDeath();
    }
}
