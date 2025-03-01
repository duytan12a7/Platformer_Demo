using UnityEngine;
using Spine;
using Spine.Unity;

public class EnemyAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private SkeletonAnimation skeletonAnimation;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();

        if (skeletonAnimation != null)
        {
            skeletonAnimation.AnimationState.Event += HandleSpineAnimationEvent;
        }
    }

    private void OnDestroy()
    {
        if (skeletonAnimation != null)
        {
            skeletonAnimation.AnimationState.Event -= HandleSpineAnimationEvent;
        }
    }

    public void AnimationEventTrigger(string eventName)
    {
        switch (eventName)
        {
            case "AttackTrigger":
                AttackTrigger();
                break;
            case "AnimationFinish":
                AnimationFinishTrigger();
                break;
            case "OpenCounterWindow":
                OpenCounterWindow();
                break;
            case "CloseCounterWindow":
                CloseCounterWindow();
                break;
            default:
                Debug.Log($"[Animator] Unhandled animation event: {eventName}");
                break;
        }
    }

    private void HandleSpineAnimationEvent(TrackEntry trackEntry, Spine.Event e)
    {
        switch (e.Data.Name)
        {
            case "Attack":
                // AttackTrigger();
                break;
            case "AnimationFinish":
                AnimationFinishTrigger();
                break;
            case "OpenCounterWindow":
                OpenCounterWindow();
                break;
            case "CloseCounterWindow":
                CloseCounterWindow();
                break;
            case "OpenSkillAttack":
                // OpenSkillAttack();
                break;
            case "CloseSkillAttack":
                break;
            case "Vibranium":
                break;
            default:
                Debug.Log($"[Spine] Unhandled animation event: {e.Data.Name}");
                break;
        }
    }

    private void AnimationFinishTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius, enemy.whatIsCharacter);

        if (colliders.Length == 0) return;

        foreach (Collider2D hit in colliders)
        {
            PlayerStats target = hit.GetComponentInChildren<PlayerStats>();
            if (target == null) continue;

            enemy.Stats.PerformAttack(target, enemy.transform);
        }
    }

    protected void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    protected void CloseCounterWindow() => enemy.CloseCounterAttackWindow();

    protected void OpenSkillAttack() => enemy.OpenSkillAttack();
    protected void CloseSkillAttack() => enemy.CloseSkillAttack();
}
