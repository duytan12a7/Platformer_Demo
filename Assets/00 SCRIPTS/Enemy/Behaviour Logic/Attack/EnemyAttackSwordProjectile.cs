using UnityEngine;
using Spine;

[CreateAssetMenu(fileName = "Attack-Sword Projectile", menuName = "Enemy Logic/Attack Logic/Sword Projectile")]
public class EnemyAttackSwordProjectile : EnemyAttackSOBase
{

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.SetZeroVelocity();

        if (enemy.IsKnocked() && enemy.Stats.CanKnock)
            enemy.StateMachine.ChangeState(enemy.HurtState);

        if (enemy.skeletonAnimation != null && enemy.skeletonAnimation.AnimationState.GetCurrent(0).IsComplete 
        || enemy.CurrentTriggerType == Enemy.AnimationTriggerType.EffectAttack)
            enemy.StateMachine.ChangeState(enemy.ChaseState);
    }
}
