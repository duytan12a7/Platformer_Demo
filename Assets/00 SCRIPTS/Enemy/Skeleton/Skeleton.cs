using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{

    protected override void Awake()
    {
        base.Awake();

        IdleState = new EnemyIdleState(this, StateMachine, Global.AnimatorParams.Idle);
        MoveState = new EnemyMoveState(this, StateMachine, Global.AnimatorParams.Move);
        BattleState = new EnemyBattleState(this, StateMachine, Global.AnimatorParams.Move);
        AttackState = new EnemyAttackState(this, StateMachine, Global.AnimatorParams.Attack);
        StunnedState = new EnemyStunnedState(this, StateMachine, Global.AnimatorParams.Hurt);
        DeadState = new EnemyDeadState(this, StateMachine, Global.AnimatorParams.Idle);
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
