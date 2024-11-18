using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Create Enemy Data")]
public class EnemyData : ScriptableObject
{

    [Header("Movement State")]
    public float MovementVelocity;
    public float IdleTimer;
}
