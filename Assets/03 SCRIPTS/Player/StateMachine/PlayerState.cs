using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected Rigidbody2D rb;

    protected bool isAnimationFinished;

    protected float stateTimer;

    private string animBoolName;

    protected float xInput;
    protected float yInput;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        player.PlayAnimation(animBoolName, true);
        rb = player.Rigid;
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {
        player.PlayAnimation(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;

        if (!player.IsMove) return;
        xInput = (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) - (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0);
        yInput = (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) - (Input.GetKey(KeyCode.DownArrow) ? 1 : 0);

        if (player.Anim == null) return;
        player.Anim.SetFloat(Global.AnimatorParams.xVelocity, rb.velocity.x);
        player.Anim.SetFloat(Global.AnimatorParams.yVelocity, rb.velocity.y);
    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
