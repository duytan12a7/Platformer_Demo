using System.Collections;
using UnityEngine;

public class EnemyDeadState : EnemyState
{

    public EnemyDeadState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName)
    {
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

    protected IEnumerator Reset()
    {
        yield return new WaitForSeconds(2f);
        enemy.Reset();
        enemy.gameObject.SetActive(false);
    }
}
