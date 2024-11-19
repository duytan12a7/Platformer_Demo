using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody2D Rigid { get; private set; }
    public EntityFX entityFX { get; private set; }
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }

    #endregion

    #region Other Variables

    [HideInInspector] public Vector2 CurrentVelocity;
    protected Vector2 workspace;
    [HideInInspector] public int FacingDirection;
    protected bool isFacingRight;

    #endregion
    #region Knockback Variables

    [Header("Knockback Variables")]
    [SerializeField] protected Vector2 knockbackVelocity;
    [SerializeField] protected float knockBackDuration;
    protected bool isKnocked;

    #endregion

    [Header("Check Collisions")]
    #region Collisions
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float groundCheckRadius = 0.08f;
    [SerializeField] protected float wallCheckDistance = 0.5f;
    [SerializeField] protected LayerMask whatIsGround;
    public Transform attackCheck;
    public float attackCheckRadius;
    public LayerMask whatIsCharacter;
    #endregion

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        Rigid = GetComponent<Rigidbody2D>();
        entityFX = GetComponentInChildren<EntityFX>();

        isFacingRight = true;
        FacingDirection = 1;
    }

    protected virtual void Update()
    {
    }

    #region Set Functions

    public virtual void SetVelocityX(float velocity)
    {
        if (isKnocked) return;

        workspace.Set(velocity, CurrentVelocity.y);
        Rigid.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public virtual void SetVelocityY(float velocity)
    {
        if (isKnocked) return;

        workspace.Set(CurrentVelocity.y, velocity);
        Rigid.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public virtual void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked) return;

        Rigid.velocity = new Vector2(xVelocity, yVelocity);
    }

    public virtual void SetVelocity(Vector2 velocity)
    {
        if (isKnocked) return;

        Rigid.velocity = velocity;
    }

    public virtual void SetZeroVelocity()
    {
        if (isKnocked) return;

        Rigid.velocity = Vector2.zero;
    }

    #endregion

    #region Check Functions

    public virtual bool IsGroundDetected() => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, wallCheckDistance, whatIsGround);


    public virtual void CheckFlip(float xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
            Flip();
    }

    #endregion

    public virtual void DamageEffect()
    {
        StartCoroutine(HitKnockback());
        entityFX.StartCoroutine(entityFX.HitFlashFX());
    }

    #region Other Functions

    public virtual void Flip()
    {
        FacingDirection *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        Rigid.velocity = new Vector2(knockbackVelocity.x * -FacingDirection, knockbackVelocity.y);

        yield return new WaitForSeconds(knockBackDuration);
        isKnocked = false;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawRay(wallCheck.position, Vector3.right * FacingDirection * wallCheckDistance);
        // Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    #endregion
}
