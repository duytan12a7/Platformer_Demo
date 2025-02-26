using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    #region States

    public GoblinIdleState IdleState { get; private set; }
    public GoblinMoveState MoveState { get; private set; }
    public GoblinBattleState BattleState { get; private set; }
    public GoblinAttackState AttackState { get; private set; }
    public GoblinStunnedState StunnedState { get; private set; }
    public GoblinDeadState DeadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        IdleState = new GoblinIdleState(this, StateMachine, "idle", this);
        MoveState = new GoblinMoveState(this, StateMachine, "move", this);
        BattleState = new GoblinBattleState(this, StateMachine, "move", this);
        AttackState = new GoblinAttackState(this, StateMachine, "skill_0", this);
        StunnedState = new GoblinStunnedState(this, StateMachine, "hurt", this);
        DeadState = new GoblinDeadState(this, StateMachine, "idle", this);
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanBeStunned()
    {
        if (!base.CanBeStunned()) return false;
        StateMachine.ChangeState(StunnedState);
        return true;
    }

    public override void Die()
    {
        base.Die();
        StateMachine.ChangeState(DeadState);
    }

    public override void Reset()
    {
        base.Reset();
        StateMachine.ChangeState(IdleState);

        DefaultFacing();
        Stats.Reset();
        CloseCounterAttackWindow();

        SetSpeedAnimation(1f);
        BoxCollider.enabled = true;
    }
}
