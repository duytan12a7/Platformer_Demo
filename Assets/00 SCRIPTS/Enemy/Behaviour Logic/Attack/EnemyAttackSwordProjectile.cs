using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Sword Projectile", menuName = "Enemy Logic/Attack Logic/Sword Projectile")]
public class EnemyAttackSwordProjectile : EnemyAttackSOBase
{
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.SetZeroVelocity();

        if (enemy.CurrentTriggerType == Enemy.AnimationTriggerType.EffectAttack)
            enemy.StateMachine.ChangeState(enemy.ChaseState);
    }
}
