using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    private int level = 0;
    private int xp = 0;
    private int xpToNextLevel = 100;
    [SerializeField] private SkillManager skillManager;

    [SerializeField] private SkillSelectionUI skillSelectionUI;

    public event Action OnDeath;

    public List<SkillCard> OwnedSkills = new();

    protected override void Start()
    {
        base.Start();
        player = GetComponentInParent<Player>();
    }

    public override void TakeDamage(int damage, Transform attacker)
    {
        if (player.IsDashing()) return;

        base.TakeDamage(damage, attacker);

        player.DamageEffect(attacker);
    }

    protected override void Die()
    {
        base.Die();

        player.Die();
        OnDeath?.Invoke();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.L))
            LevelUp();
    }

    public void GainXP(int amount)
    {
        xp += amount;
        if (xp >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        xp = 0;
        xpToNextLevel += 50;
        skillSelectionUI.ShowSkillSelection(skillManager.GetRandomSkills(3));
    }

}
