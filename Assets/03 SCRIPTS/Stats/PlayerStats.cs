using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [Header(" Level Stats")]
    private int level = 0;
    private int xp = 0;
    private int xpToNextLevel;
    public int GetCurrentXP() => xp;
    public int GetXPToNextLevel() => xpToNextLevel;
    private List<int> expTable = new();
    private Queue<int> pendingLevelUps = new();

    [Header(" SkillCard ")]
    [SerializeField] private SkillManager skillManager;
    [SerializeField] private SkillSelectionUI skillSelectionUI;
    public List<SkillCard> OwnedSkills = new();
    private bool isSelectingSkill = false;

    [Header(" Events ")]
    private Player player;
    public event Action OnDeath;
    public event Action<int> OnLevelUp;

    protected override void Start()
    {
        base.Start();
        player = GetComponentInParent<Player>();

        InitializeExpTable();
        xpToNextLevel = expTable[level];

        GameEvent.OnGainExp += GainXP;
        Invoke(nameof(LevelUp), 1.5f);
    }

    private void InitializeExpTable()
    {
        int totalExp = 0;
        for (int i = 1; i <= 30; i++)
        {
            totalExp += i * 100;
            expTable.Add(totalExp);
        }
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

    public void GainXP(int amount)
    {
        xp += amount;
        GameEvent.CallOnExpChanged();

        while (level < expTable.Count && xp >= xpToNextLevel)
        {
            xp -= xpToNextLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        pendingLevelUps.Enqueue(level + 1);
        TryShowSkillSelection();
    }

    private void TryShowSkillSelection()
    {
        if (isSelectingSkill || pendingLevelUps.Count == 0) return;

        isSelectingSkill = true;
        level = pendingLevelUps.Dequeue();

        if (level < expTable.Count)
        {
            xpToNextLevel = expTable[level - 1];
        }

        skillSelectionUI.ShowSkillSelection(skillManager.GetRandomSkills(3), OnSkillSelected);
        OnLevelUp?.Invoke(level);
    }

    private void OnSkillSelected()
    {
        isSelectingSkill = false;
        TryShowSkillSelection();
    }

    private void OnDestroy() => GameEvent.OnGainExp -= GainXP;
}
