using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Enemy Logic/Chase Logic/ Direct Chase")]
public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
{
    [Range(0f, 5f)][SerializeField] private float _movementSpeed;
    [Range(0f, 5f)][SerializeField] private float _timeTillExit;
    [Range(0f, 5f)][SerializeField] private float _distanceToCountExit;

    private float timer;

    public override void Enter()
    {
        base.Enter();
        timer = 0f;
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

        if (Vector2.Distance(playerTransform.position, enemy.transform.position) > _distanceToCountExit)
        {
            timer += Time.deltaTime;
            if (timer > _timeTillExit)
                enemy.StateMachine.ChangeState(enemy.IdleState);
        }
        else timer = 0f;
    }
}
