using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    #region States

    #endregion

    protected override void Awake()
    {
        base.Awake();

        IdleState = new GoblinIdleState(this, StateMachine, "idle");
        MoveState = new GoblinMoveState(this, StateMachine, "move");
        BattleState = new GoblinBattleState(this, StateMachine, "move");
        AttackState = new GoblinAttackState(this, StateMachine, "skill_0");
        StunnedState = new GoblinStunnedState(this, StateMachine, "hurt");
        DeadState = new GoblinDeadState(this, StateMachine, "idle");
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
}
