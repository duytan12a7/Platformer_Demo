using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Single-Straight Projectile", menuName = "Enemy Logic/Attack Logic/Single Straight Projectile")]
public class EnemyAttackSingleStraightProjectile : EnemyAttackSOBase
{
    [SerializeField] private Rigidbody2D _bulletPrefab;
    [SerializeField] private float _timeBetweenShot;
    [SerializeField] private float _bulletSpeed;

    public override void Enter()
    {
        base.Enter();
        stateTimer = _timeBetweenShot;
    }

    public override void Update()
    {
        base.Update();

        enemy.MoveEnemy(Vector2.zero);

        if (stateTimer > 0) return;

        Vector2 dir = (playerTransform.position - enemy.transform.position).normalized;

        Rigidbody2D bullet = GameObject.Instantiate(_bulletPrefab, enemy.transform.position, Quaternion.identity);
        bullet.velocity = dir * _bulletSpeed;

        enemy.StateMachine.ChangeState(enemy.ChaseState);
    }
}
