using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody2D Rigid { get; private set; }

    #endregion

    #region Other Variables

    [HideInInspector] public Vector2 CurrentVelocity;
    protected Vector2 workspace;
    [HideInInspector] public int FacingDirection;

    #endregion

    #region Check Transform
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float groundCheckRadius = 0.08f;
    [SerializeField] protected float wallCheckDistance = 0.5f;
    [SerializeField] protected LayerMask whatIsGround;
    #endregion

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        Rigid = GetComponent<Rigidbody2D>();

        FacingDirection = 1;
    }

    protected virtual void Update()
    {
    }

    #region Set Functions

    public virtual void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        Rigid.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public virtual void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.y, velocity);
        Rigid.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public virtual void SetVelocity(float xVelocity, float yVelocity)
    {
        Rigid.velocity = new Vector2(xVelocity, yVelocity);
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

    #region Other Functions

    public virtual void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(wallCheck.position, Vector3.right * FacingDirection * wallCheckDistance);
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    #endregion
}
