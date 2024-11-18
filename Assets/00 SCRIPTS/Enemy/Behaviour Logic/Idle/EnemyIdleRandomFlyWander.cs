using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Fly Wander", menuName = "Enemy Logic/Idle Logic/Random Fly Wander")]
public class EnemyIdleRandomFlyWander : EnemyIdleSOBase
{
    [Range(0f, 5f)][SerializeField] private float _randomMovementRange;
    [Range(0f, 5f)][SerializeField] private float _movementSpeed;
    private Vector3 _targetPos;
    private Vector3 _direction;

    public override void Enter()
    {
        base.Enter();

        _targetPos = GetRandomPointInCircle();
        enemy.Anim.SetBool("Move", true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Anim.SetBool("Move", false);
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsGroundDetected())
            ChangeDirection();

        _direction = (_targetPos - enemy.transform.position).normalized;

        enemy.MoveEnemy(_direction * _movementSpeed);
        enemy.CheckFlip(_direction.x);

        if ((enemy.transform.position - _targetPos).sqrMagnitude < 0.01f)
            _targetPos = GetRandomPointInCircle();
    }

    private Vector3 GetRandomPointInCircle() => enemy.transform.position + (Vector3)Random.insideUnitCircle * _randomMovementRange;

    private void ChangeDirection()
    {
        enemy.Flip();
        _targetPos = GetRandomPointInCircle();

        _direction = (_targetPos - enemy.transform.position).normalized;
    }
}
