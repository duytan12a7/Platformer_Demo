using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "Wander Ground", menuName = "Enemy Logic/Wander Logic/Wander Ground")]
public class EnemyWanderGround : EnemyWanderSOBase
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float minChangeDirectionTime, maxChangeDirectionTime;

    private float changeDirectionTimer;

    public override void Enter()
    {
        base.Enter();
        ResetChangeDirectionTimer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.SetVelocityX(enemy.FacingDirection * movementSpeed);

        if (!enemy.IsGroundDetected() || enemy.IsWallDetected())
        {
            enemy.Flip();
            ResetChangeDirectionTimer();
        }

        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            enemy.Flip();
            ResetChangeDirectionTimer();
        }
    }

    private void ResetChangeDirectionTimer()
    {
        changeDirectionTimer = Random.Range(minChangeDirectionTime, maxChangeDirectionTime);
    }
}
