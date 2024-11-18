using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Wander Ground", menuName = "Enemy Logic/Idle Logic/Wander Ground")]
public class EnemyIdleWanderGround : EnemyIdleSOBase
{
    [SerializeField] private float _movementSpeed;

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.SetVelocityX(enemy.FacingDirection * _movementSpeed);

        if (!enemy.IsGroundDetected() || enemy.IsWallDetected())
            enemy.Flip();
    }
}
