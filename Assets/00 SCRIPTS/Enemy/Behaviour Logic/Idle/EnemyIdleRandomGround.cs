using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Ground", menuName = "Enemy Logic/Idle Logic/Random Ground")]
public class EnemyIdleRandomGround : EnemyIdleSOBase
{
    [SerializeField] private float _movementSpeed = 2f;

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }


    public override void Enter()
    {
        base.Enter();
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

        enemy.SetVelocityX(enemy.FacingDirection * _movementSpeed);

        if (enemy.IsWallDetected())
            enemy.Flip();
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
