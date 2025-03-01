using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBattleState : EnemyState
{
    private readonly Goblin enemy;
    private Transform playerTransform;

    public GoblinBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Goblin enemy)
        : base(enemy, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        playerTransform = GameManager.Instance.Player.transform;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.BattleTime;
            if (enemy.CheckAttackDistance())
                enemy.StateMachine.ChangeState(enemy.AttackState);
        }
        else if (stateTimer < 0 || Vector2.Distance(playerTransform.position, enemy.transform.position) > 7)
            stateMachine.ChangeState(enemy.IdleState);

        float directionX = (playerTransform.position.x - enemy.transform.position.x) > 0 ? 1 : -1;
        enemy.SetVelocityX(directionX * enemy.MoveSpeed);
        enemy.CheckFlip(directionX);
    }
}
