using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-DirectX Chase", menuName = "Enemy Logic/Chase Logic/ Direct Chase X")]
public class EnemyChaseDirectXToPlayer : EnemyChaseSOBase
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _timeTillExit;
    [SerializeField] private float _distanceToCountExit;

    private float _timer;

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void Enter()
    {
        base.Enter();
        _timer = 0f;
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
        enemy.SetVelocityX(direction.x * _movementSpeed);
        enemy.CheckFlip(direction.x);

        if (Vector2.Distance(playerTransform.position, enemy.transform.position) > _distanceToCountExit)
        {
            _timer += Time.deltaTime;
            if (_timer > _timeTillExit)
                enemy.StateMachine.ChangeState(enemy.IdleState);
        }
        else _timer = 0f;
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
