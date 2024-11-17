using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Single-Straight Projectile", menuName = "Enemy Logic/Attack Logic/Single Straight Projectile")]
public class EnemyAttackSingleStraightProjectile : EnemyAttackSOBase
{
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float timeBetweenShot;
    [SerializeField] private float bulletSpeed;

    public override void Enter()
    {
        base.Enter();
        stateTimer = timeBetweenShot;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.SetZeroVelocity();

        if (stateTimer > 0) return;

        Vector2 dir = (playerTransform.position - enemy.transform.position).normalized;

        Rigidbody2D bullet = GameObject.Instantiate(bulletPrefab, enemy.transform.position, Quaternion.identity);
        bullet.velocity = dir * bulletSpeed;

        enemy.StateMachine.ChangeState(enemy.ChaseState);
    }
}
