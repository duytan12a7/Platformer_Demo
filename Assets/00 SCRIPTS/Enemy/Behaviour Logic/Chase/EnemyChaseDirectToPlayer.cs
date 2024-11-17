using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Enemy Logic/Chase Logic/ Direct Chase")]
public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float timeTillExit;

    public override void Enter()
    {
        base.Enter();
        stateTimer = timeTillExit;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Vector2 direction = (playerTransform.position - enemy.transform.position).normalized;
        enemy.SetVelocity(direction * movementSpeed);
        enemy.CheckFlip(direction.x);

        if (!enemy.CheckAggroRadius() && stateTimer < 0)
            enemy.StateMachine.ChangeState(enemy.IdleState);
    }
}
