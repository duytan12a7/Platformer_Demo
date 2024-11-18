using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-DirectX Chase", menuName = "Enemy Logic/Chase Logic/ Direct Chase X")]
public class EnemyChaseDirectXToPlayer : EnemyChaseSOBase
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _timeTillExit;

    public override void Enter()
    {
        base.Enter();
        enemy.Anim.SetBool("Move", true);
        stateTimer = _timeTillExit;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Anim.SetBool("Move", false);
    }

    public override void Update()
    {
        base.Update();

        Vector2 direction = (playerTransform.position - enemy.transform.position).normalized;
        enemy.SetVelocityX(direction.x * _movementSpeed);
        enemy.CheckFlip(direction.x);

        if (!enemy.IsAggroed && stateTimer < 0)
            enemy.StateMachine.ChangeState(enemy.IdleState);
    }
}
