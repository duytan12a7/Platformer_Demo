using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Enemy Logic/Chase Logic/ Direct Chase")]
public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _timeTillExit;
    [SerializeField] private float _distanceToCountExit;

    private float timer;

    public override void Enter()
    {
        base.Enter();
        stateTimer = _timeTillExit;
        enemy.Anim.SetBool("Move", true);
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
        enemy.MoveEnemy(direction * _movementSpeed);
        enemy.CheckFlip(direction.x);

        if (!enemy.IsAggroed && stateTimer < 0)
            enemy.StateMachine.ChangeState(enemy.IdleState);

        // if (Vector2.Distance(playerTransform.position, enemy.transform.position) > _distanceToCountExit)
        // {
        //     timer += Time.deltaTime;
        //     if (timer > _timeTillExit)
        //         enemy.StateMachine.ChangeState(enemy.IdleState);
        // }
        // else timer = 0f;
    }
}
