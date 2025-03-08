using Spine;
using Spine.Unity;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public int comboCounter;
    private float lastTimeAttacked;
    private bool isAttackingMove;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
        : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCounter > playerData.ComboWindow || Time.time >= lastTimeAttacked + 0.5f)
            comboCounter = 0;

        player.Anim.SetInteger(Global.AnimatorParams.ComboCounter, comboCounter);
        stateTimer = playerData.AttackDuration;
        player.fxAttack[comboCounter].Play();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
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
