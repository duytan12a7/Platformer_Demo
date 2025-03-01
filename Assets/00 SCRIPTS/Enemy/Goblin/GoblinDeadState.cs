using System.Collections;
using UnityEngine;

public class GoblinDeadState : EnemyState
{
    private Goblin enemy;

    public GoblinDeadState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Goblin enemy)
        : base(enemy, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.PlayAnimation(enemy.lastAnimBoolName, true);
        enemy.SetSpeedAnimation(0f);
        enemy.BoxCollider.enabled = false;

        stateTimer = 0.1f;

        enemy.StartCoroutine(Reset());
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateTimer > 0 && rb != null)
            rb.velocity = new Vector2(0f, 10f);
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(2f);
        enemy.Reset();
        enemy.gameObject.SetActive(false);
    }
}
