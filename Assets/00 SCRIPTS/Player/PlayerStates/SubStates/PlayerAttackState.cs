using Spine;
using Spine.Unity;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
        : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCounter > playerData.ComboWindow || Time.time >= lastTimeAttacked + playerData.ComboWindow)
            comboCounter = 0;

        player.Anim.SetInteger(Global.AnimatorParams.ComboCounter, comboCounter);
        stateTimer = playerData.AttackDuration;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (stateTimer > 0 && player.IsGroundDetected())
            player.SetVelocity(0f, 0f);
        else if (isAnimationFinished)
            stateMachine.ChangeState(player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAttacked = Time.time;
        player.GetComponentInChildren<PlayerDamageSender>()?.ClearAttackedEnemies();
    }
}
