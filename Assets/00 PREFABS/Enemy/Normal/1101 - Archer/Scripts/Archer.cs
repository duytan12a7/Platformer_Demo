using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{

    [Header("Archer spisifc info")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float arrowDamage;

    protected override void Awake()
    {
        base.Awake();

        IdleState = new ArcherIdleState(this, StateMachine, Global.AnimatorParams.Idle);
        MoveState = new ArcherMoveState(this, StateMachine, Global.AnimatorParams.Move);
        BattleState = new ArcherBattleState(this, StateMachine, Global.AnimatorParams.Move);
        AttackState = new ArcherAttackState(this, StateMachine, Global.AnimatorParams.Attack);
        StunnedState = new ArcherStunnedState(this, StateMachine, Global.AnimatorParams.Hurt);
        DeadState = new ArcherDeadState(this, StateMachine, Global.AnimatorParams.Idle);
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

    public override void AnimationSpecialAttackTrigger()
    {
        GameObject newArrow = Instantiate(arrowPrefab, attackCheck.position, Quaternion.identity);
        newArrow.GetComponent<ArrowController>().SetupArrow(arrowSpeed * FacingDirection, Stats);
    }
}
