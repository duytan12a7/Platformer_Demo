using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Fly Wander", menuName = "Enemy Logic/Idle Logic/Random Fly Wander")]
public class EnemyIdleRandomFlyWander : EnemyIdleSOBase
{
    [SerializeField] private float randomMovementRange;
    [SerializeField] private float movementSpeed;
    private Vector3 targetPos;
    private Vector3 direction;

    public override void Enter()
    {
        base.Enter();

        targetPos = GetRandomPointInCircle();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.IsGroundDetected())
            ChangeDirection();

        direction = (targetPos - enemy.transform.position).normalized;

        enemy.SetVelocity(direction * movementSpeed);
        enemy.CheckFlip(direction.x);

        if ((enemy.transform.position - targetPos).sqrMagnitude < 0.01f)
            targetPos = GetRandomPointInCircle();
    }

    private Vector3 GetRandomPointInCircle() => enemy.transform.position + (Vector3)Random.insideUnitCircle * randomMovementRange;

    private void ChangeDirection()
    {
        enemy.Flip();
        targetPos = GetRandomPointInCircle();

        direction = (targetPos - enemy.transform.position).normalized;
    }
}
