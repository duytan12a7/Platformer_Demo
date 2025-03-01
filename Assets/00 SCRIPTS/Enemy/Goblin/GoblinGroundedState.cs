using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinGroundedState : EnemyState
{
    private Goblin enemy;
    private Transform playerTransform;

    public GoblinGroundedState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Goblin enemy) : base(enemy, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        playerTransform = GameManager.Instance.Player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
