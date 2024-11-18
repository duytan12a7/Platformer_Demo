using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    public SkeletonIdleState IdleState { get; private set; }
    public SkeletonMoveState MoveState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        IdleState = new SkeletonIdleState(this, StateMachine, enemyData, "Idle", this);
        MoveState = new SkeletonMoveState(this, StateMachine, enemyData, "Move", this);
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
}