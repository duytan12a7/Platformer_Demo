using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageSender : MonoBehaviour
{
    [SerializeField] private Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerStats target = collider?.GetComponentInChildren<PlayerStats>();
        if (target == null) return;

        player.Stats.PerformAttack(target, transform);
    }
}
