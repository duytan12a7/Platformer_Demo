using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    private void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        _enemy.AnimationTriggerEvent(triggerType);
    }
}
