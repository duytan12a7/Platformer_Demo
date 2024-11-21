using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 1 || Time.time >= lastTimeAttacked + playerData.ComboWindow)
            comboCounter = 0;

        player.Anim.SetInteger(Global.AnimatorParams.ComboCounter, comboCounter);

        player.SetVelocity(playerData.AttackMovement[comboCounter].x * player.FacingDirection, playerData.AttackMovement[comboCounter].y);

        stateTimer = playerData.AttackDuration;
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateTimer < 0 && player.IsGroundDetected())
            player.SetVelocity(0f, 0f);

        if (isAnimationFinished)
            stateMachine.ChangeState(player.IdleState);
    }
}
