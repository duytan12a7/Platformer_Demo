using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;

    protected override void Start()
    {
        base.Start();
        enemy = GetComponentInParent<Enemy>();
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        enemy.DamageEffect();
    }

    protected override void Die()
    {
        enemy.StateMachine.ChangeState(enemy.DieState);
    }
}
