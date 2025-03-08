using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrukAttackState : EnemyAttackState
{
    private int attackType;
    private int currentComboIndex;
    private static readonly string[] comboAnimations = { "attack_1_0", "attack_1_1", "attack_1_2" };
    private float dashTimer;

    public GrukAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, int attackType)
        : base(enemy, stateMachine, animBoolName)
    {
        this.attackType = attackType;
        currentComboIndex = 0;
    }

    public override void Enter()
    {
        base.Enter();

        if (enemy.skeletonAnimation == null) return;

        if (attackType == 1)
            enemy.skeletonAnimation.AnimationState.SetAnimation(0, "attack_0", false);
        else if (attackType == 2)
        {
            currentComboIndex = 0;
            enemy.skeletonAnimation.AnimationState.SetAnimation(0, comboAnimations[currentComboIndex], false);
        }
    }

    public override void LogicUpdate()
    {
        if (attackType == 2 && comboAnimations[currentComboIndex] == "attack_1_1")
            HandleDash();

        if (enemy.skeletonAnimation != null && enemy.skeletonAnimation.AnimationState.GetCurrent(0).IsComplete)
            PlayNextCombo();
    }

    private void HandleDash()
    {
        if (dashTimer <= 0)
            dashTimer = enemy.DashDuration;

        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
            enemy.SetVelocityX(enemy.FacingDirection * enemy.DashSpeed);
        }
        else
            enemy.SetZeroVelocity();
    }

    private void PlayNextCombo()
    {
        if (attackType == 2 && currentComboIndex < comboAnimations.Length - 1)
        {
            currentComboIndex++;
            enemy.skeletonAnimation.AnimationState.SetAnimation(0, comboAnimations[currentComboIndex], false);
        }
        else
            stateMachine.ChangeState(enemy.IdleState);
    }
}
