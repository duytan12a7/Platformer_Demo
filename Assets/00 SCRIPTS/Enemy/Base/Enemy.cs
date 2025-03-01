using System;
using Spine.Unity;
using UnityEngine;
using Spine;

public class Enemy : Entity
{
    protected ICharacterAnimation characterAnimation;
    public SkeletonAnimation skeletonAnimation;
    // #region State Machine Variables

    public EnemyStateMachine StateMachine { get; private set; }

    // #endregion

    #region Components

    public EnemyStats Stats { get; private set; }

    #endregion

    #region Other Variables
    public AnimationTriggerType CurrentTriggerType { get; private set; }

    #endregion

    public Action OnFlipped;

    [Header("Stunned info")]
    public float StunDuration;
    public Vector2 StunDirection;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;

    [Header("Move info")]
    public float MoveSpeed = 2f;
    public float IdleTime = 1f;
    public float BattleTime = 7f;
    private float DefaultMoveSpeed;

    [Header("Attack info")]
    public float agroDistance = 2;
    public float attackDistance = 1.5f;
    public float attackCheckDistance = 5f;

    public string lastAnimBoolName { get; private set; }

    #region Unity Callback Functions


    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EnemyStateMachine();

        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();

        if (skeletonAnimation != null)
            characterAnimation = new SpineCharacterAnimation(skeletonAnimation);
        else if (Anim != null)
            characterAnimation = new AnimatorCharacterAnimation(Anim);
    }

    protected override void Start()
    {
        base.Start();

        Stats = GetComponentInChildren<EnemyStats>();
    }

    protected override void Update()
    {
        base.Update();

        StateMachine.CurrentState.LogicUpdate();
    }

    #endregion

    #region Check Functions

    public override void CheckFlip(float xInput)
    {
        if (isKnocked) return;

        if (xInput < 0f && isFacingRight || xInput > 0f && !isFacingRight)
            Flip();
    }

    public override void Flip()
    {
        base.Flip();
        OnFlipped();
    }



    public virtual RaycastHit2D IsPlayerDetected()
    {
        RaycastHit2D rightDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, attackCheckDistance, whatIsCharacter);
        RaycastHit2D leftDetected = Physics2D.Raycast(wallCheck.position, Vector2.left * FacingDirection, attackCheckDistance, whatIsCharacter);
        RaycastHit2D wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, attackCheckDistance, whatIsGround);

        if (leftDetected)
            return leftDetected;

        if (wallDetected)
        {
            if (wallDetected.distance < rightDetected.distance)
                return default(RaycastHit2D);
        }

        return rightDetected;
    }

    public bool CheckAttackDistance() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, attackDistance, whatIsCharacter);

    #endregion

    #region Animation Trigger Functions

    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        CurrentTriggerType = triggerType;
        StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
    }

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public enum AnimationTriggerType
    {
        None,
        EffectAttack,
        EnemyDamaged,
        PlayerFootstepsound
    }

    #endregion

    #region Other Functions

    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }

    public virtual void AssignLastAnimName(string _animBoolName) => lastAnimBoolName = _animBoolName;

    public virtual void Reset()
    {
    }

    #endregion

    #region Skeleton Animation

    public virtual void PlayAnimation(string animationName, bool loop = false)
    {
        if (characterAnimation == null) return;
        characterAnimation.PlayAnimation(animationName, loop);
    }

    public virtual void SetSpeedAnimation(float speed)
    {
        if (characterAnimation == null) return;
        characterAnimation.SetSpeedAnimation(speed);
    }

    public virtual void SetTrigger(string triggerName)
    {
        if (characterAnimation == null) return;
        characterAnimation.SetTrigger(triggerName);
    }

    public virtual void StopAnimation()
    {
        if (characterAnimation == null) return;
        characterAnimation.StopAnimation();
    }

    #endregion

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Debug.DrawRay(wallCheck.position, Vector2.right * FacingDirection * attackCheckDistance);
        Debug.DrawRay(wallCheck.position, Vector2.left * FacingDirection * attackCheckDistance);
    }
}
