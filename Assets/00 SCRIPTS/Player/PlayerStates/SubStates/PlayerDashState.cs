using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private bool canDash;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        canDash = true;
    }

    public override void Enter()
    {
        base.Enter();
        player.StartCoroutine(Dash());
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.WallSlideState);
    }

    public IEnumerator Dash()
    {
        canDash = false;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        player.SetVelocityX(playerData.DashSpeed * player.FacingDirection);

        yield return new WaitForSeconds(playerData.DashDuration);
        rb.gravityScale = originalGravity;

        stateMachine.ChangeState(player.IdleState);

        yield return new WaitForSeconds(playerData.DashCoolDown);
        canDash = true;
    }

    public bool CanDash() => canDash && !player.IsWallDetected();
}
