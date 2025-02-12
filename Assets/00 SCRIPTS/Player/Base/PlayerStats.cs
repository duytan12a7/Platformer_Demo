using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    protected override void Start()
    {
        base.Start();
        player = GetComponentInParent<Player>();
    }

    public override void TakeDamage(int damage)
    {
        if (player.IsDashing()) return;

        base.TakeDamage(damage);
        player.DamageEffect();
    }

    protected override void Die()
    {
        player.StateMachine.ChangeState(player.DieState);
    }
}
