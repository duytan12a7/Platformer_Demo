using UnityEngine;

[CreateAssetMenu(fileName = "Chase-DirectX Chase", menuName = "Enemy Logic/Chase Logic/ Direct Chase X")]
public class EnemyChaseDirectXToPlayer : EnemyChaseSOBase
{
    [SerializeField] private float timeTillExit;

    public override void Enter()
    {
        base.Enter();
        stateTimer = timeTillExit;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Vector2 direction = (playerTransform.position - enemy.transform.position).normalized;
        enemy.SetVelocityX(direction.x * enemy.Stats.CurrentMovementSpeed);
        enemy.CheckFlip(direction.x);

        if (!enemy.CheckAggroDistance() && stateTimer < 0)
            enemy.StateMachine.ChangeState(enemy.WanderState);
        else if (!enemy.IsGroundDetected())
            enemy.SetZeroVelocity();
    }
}
