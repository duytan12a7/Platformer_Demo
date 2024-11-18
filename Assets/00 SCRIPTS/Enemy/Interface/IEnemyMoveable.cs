using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMoveable
{
    Rigidbody2D Rigid { get; set; }
    int FacingDirection { get; set; }
    void SetVelocity(Vector2 velocity);
    void CheckFlip(float xVelocity);
}
