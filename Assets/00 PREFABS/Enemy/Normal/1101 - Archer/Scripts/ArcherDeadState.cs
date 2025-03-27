using System.Collections;
using UnityEngine;

public class ArcherDeadState : EnemyDeadState
{

    public ArcherDeadState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
