using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Single-Straight Projectile", menuName = "Enemy Logic/Attack Logic/Single Straight Projectile")]
public class EnemyAttackSingleStraightProjectile : EnemyAttackSOBase
{
    [SerializeField] private BulletBase bulletPrefab;
    [SerializeField] private float timeBetweenShot;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifeTime;

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

        BulletBase newBullet = PoolManager.Instance.SpawnObject<BulletBase>(bulletPrefab);

        newBullet.transform.SetPositionAndRotation(enemy.transform.position, Quaternion.identity);
        newBullet.gameObject.SetActive(true);

        newBullet.Initialize(dir, bulletSpeed, lifeTime);

        enemy.StateMachine.ChangeState(enemy.ChaseState);
    }
}
