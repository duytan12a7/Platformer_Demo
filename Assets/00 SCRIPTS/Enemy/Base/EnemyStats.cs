using System;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public event Action OnDeath;
    private Enemy enemy;
    public bool CanKnock = true;

    protected override void Start()
    {
        base.Start();
        enemy = GetComponentInParent<Enemy>();
    }
    public override void TakeDamage(int damage)
    {
        enemy.EnterHurtState();
        base.TakeDamage(damage);
        enemy.DamageEffect();
    }

    protected override void Die()
    {
        enemy.StateMachine.ChangeState(enemy.DieState);
        OnDeath?.Invoke();
    }

}
