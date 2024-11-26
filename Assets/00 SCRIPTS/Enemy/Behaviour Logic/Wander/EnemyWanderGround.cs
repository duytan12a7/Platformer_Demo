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

        changeDirectionTimer -= Time.deltaTime;

        enemy.SetVelocityX(enemy.FacingDirection * movementSpeed);

        if (ShouldFlipDirection())
            HandleDirectionChange();
    }

    private bool ShouldFlipDirection() => !enemy.IsGroundDetected() || enemy.IsWallDetected() || changeDirectionTimer <= 0;

    private void ResetChangeDirectionTimer() => changeDirectionTimer = Random.Range(minChangeDirectionTime, maxChangeDirectionTime);

    private void HandleDirectionChange()
    {
        enemy.Flip();
        ResetChangeDirectionTimer();
        enemy.StateMachine.ChangeState(enemy.IdleState);
    }
}
