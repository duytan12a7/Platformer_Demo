using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Wander Ground", menuName = "Enemy Logic/Idle Logic/Wander Ground")]
public class EnemyIdleWanderGround : EnemyIdleSOBase
{
    [Range(0f, 5f)][SerializeField] private float _movementSpeed;

    public override void Enter()
    {
        base.Enter();
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

        enemy.SetVelocityX(enemy.FacingDirection * _movementSpeed);
        
        if (!enemy.IsGroundDetected() || enemy.IsWallDetected())
            enemy.Flip();
    }
}
