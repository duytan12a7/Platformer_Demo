using UnityEngine;
using Spine;
using Spine.Unity;

public class EnemySpineEventHandler : MonoBehaviour
{
    private Enemy enemy;
    private SkeletonAnimation skeletonAnimation;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        skeletonAnimation = GetComponentInParent<SkeletonAnimation>();

        if (skeletonAnimation != null)
            skeletonAnimation.AnimationState.Event += HandleSpineEvent;
        else
            Debug.LogError("[EnemySpineEventHandler] Không tìm thấy SkeletonAnimation trên Enemy!");
    }

    private void OnDestroy()
    {
        if (skeletonAnimation != null)
            skeletonAnimation.AnimationState.Event -= HandleSpineEvent;
    }

    private void HandleSpineEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "Attack")
            AttackTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius, enemy.whatIsCharacter);

        if (colliders.Length == 0) return;

        foreach (Collider2D hit in colliders)
        {
            PlayerStats target = hit.GetComponentInChildren<PlayerStats>();
            if (target == null) continue;

            enemy.Stats.PerformAttack(target);
        }
    }
}
