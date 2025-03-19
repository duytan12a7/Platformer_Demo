using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool DashInput { get; private set; }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
            AttackInput = true;

        if (context.canceled)
            AttackInput = false;
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
            DashInput = true;

        if (context.canceled)
            DashInput = false;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
            JumpInput = true;

        // if (context.performed)
        //     Debug.Log("Jump is being held down");

        // if (context.canceled)
        //     Debug.Log("Jump button has been released");
    }

    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;
}
