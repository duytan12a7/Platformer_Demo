using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Sword Projectile", menuName = "Enemy Logic/Attack Logic/Sword Projectile")]
public class EnemyAttackSwordProjectile : EnemyAttackSOBase
{

    public override void Enter()
    {
        base.Enter();
        enemy.Anim.SetBool("Attack", true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Anim.SetBool("Attack", false);
    }

    public override void Update()
    {
        base.Update();

        enemy.MoveEnemy(Vector2.zero);

        if (enemy.CurrentTriggerType == Enemy.AnimationTriggerType.EffectAttack)
            enemy.StateMachine.ChangeState(enemy.ChaseState);
    }
}
