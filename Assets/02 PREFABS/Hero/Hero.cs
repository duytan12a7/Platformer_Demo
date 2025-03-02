using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Player
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void InitializeStateMachine()
    {
        IdleState = new HeroIdleState(this, StateMachine, playerData, "idle");
        MoveState = new HeroMoveState(this, StateMachine, playerData, "move");
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
