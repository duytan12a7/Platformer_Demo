using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void AnimationFinishTrigger()
    {
        player.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius, player.whatIsCharacter);

        foreach (Collider2D hit in colliders)
        {
            if (!hit.GetComponent<Enemy>()) return;

            hit.GetComponent<Enemy>().Damage(10f);
        }
    }
}
