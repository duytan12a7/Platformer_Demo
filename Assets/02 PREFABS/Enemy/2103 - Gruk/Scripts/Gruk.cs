using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gruk : Enemy
{

    protected override void Awake()
    {
        base.Awake();

        IdleState = new GrukIdleState(this, StateMachine, "idle");
        MoveState = new GrukMoveState(this, StateMachine, "move");
        BattleState = new GrukBattleState(this, StateMachine, "move");
        AttackState = new GrukAttackState(this, StateMachine, "attack_0", 1);
        StunnedState = new GrukStunnedState(this, StateMachine, "hurt");
        DeadState = new GrukDeadState(this, StateMachine, "idle");
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
