using UnityEngine;

public class EnemyStats : CharacterStats
{
    public GameObject[] lootPrefabs;
    public event System.Action OnDeath;
    private Enemy enemy;
    public bool CanKnock = true;

    protected override void Start()
    {
        base.Start();
        enemy = GetComponentInParent<Enemy>();
    }

    public override void TakeDamage(int damage, Transform attacker)
    {
        base.TakeDamage(damage, attacker);

        if (CurrentHealth > 0)
            enemy.DamageEffect(attacker);
    }


    protected override void Die()
    {
        base.Die();

        enemy.Die();
        OnDeath?.Invoke();
    }
}
