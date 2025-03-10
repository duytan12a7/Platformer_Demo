using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/Create Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement State")]
    public float MovementVelocity;

    [Header("Jump State")]
    public float JumpVelocity;
    public int AmountOfJumps;

    [Header("Dash State")]
    public float DashSpeed;
    public float DashDuration;
    public float DashCoolDown;

    [Header("Attack State")]
    public Vector2[] AttackMovement;
    public float CounterAttackDuration;
    public int ComboWindow;
    public float AttackDuration;

    [Header("Wall State")]
    public float WallSlideVelocity;
    public float WallJumpDuration;
    public float WallJumpVelocityX;

    [Header("InAir State")]
    public float CoyoteTime;
    public float JumpHeightMultiplier;
}
