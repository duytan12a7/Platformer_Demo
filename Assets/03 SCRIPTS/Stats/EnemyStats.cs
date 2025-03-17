using UnityEngine;

public class EnemyStats : CharacterStats
{
    public GameObject[] lootPrefabs;
    public event System.Action OnDeath;
    private Enemy enemy;
    public bool CanKnock = true;

    [Header(" Level Details")]
    private int level;

    [Range(0f, 1f)]
    [SerializeField] private float percentageModifier = 0.4f;

    private void OnEnable()
    {
        level = GameManager.Instance.Level;
        ApplyLevelModifier();
        Reset();
    }

    protected override void Start()
    {
        base.Start();
        enemy = GetComponentInParent<Enemy>();
    }

    private void ApplyLevelModifier()
    {
        Modifier(Strength);
        Modifier(Agility);
        Modifier(Intelligence);
        Modifier(Vitality);

        Modifier(PhysicalDamage);
        Modifier(CriticalChance);
        Modifier(CriticalPower);

        Modifier(MaxHealth);
        Modifier(Armor);
        Modifier(Evasion);
        Modifier(MagicResistance);

        Modifier(FireDamage);
        Modifier(IceDamage);
        Modifier(LightningDamage);
    }

    private void Modifier(Stat _stat)
    {
        _stat.ResetModifiers();
        for (int i = 1; i < level; i++)
        {
            float modifier = _stat.GetValue() * percentageModifier;
            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
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
