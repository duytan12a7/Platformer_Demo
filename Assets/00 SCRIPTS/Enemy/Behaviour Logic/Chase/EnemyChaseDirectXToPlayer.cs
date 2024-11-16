using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-DirectX Chase", menuName = "Enemy Logic/Chase Logic/ Direct Chase X")]
public class EnemyChaseDirectXToPlayer : EnemyChaseSOBase
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _timeTillExit;

    public override void Enter()
    {
        base.Enter();
        stateTimer = _timeTillExit;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Vector2 direction = (playerTransform.position - enemy.transform.position).normalized;
        enemy.SetVelocityX(direction.x * _movementSpeed);
        enemy.CheckFlip(direction.x);

        if (!enemy.CheckAggroDistance() && stateTimer < 0 || !enemy.IsGroundDetected())
            enemy.StateMachine.ChangeState(enemy.IdleState);
    }
}
