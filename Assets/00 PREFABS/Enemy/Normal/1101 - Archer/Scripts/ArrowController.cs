using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float xVelocity;
    private CharacterStats stats;

    private void Update()
    {
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    public void SetupArrow(float _speed, CharacterStats _stats)
    {
        xVelocity = _speed;
        stats = _stats;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Global.LayerMask.Player))
        {
            PlayerStats target = collision.GetComponentInChildren<PlayerStats>();
            if (target == null) return;
            stats.DoDamage(target, transform);
            Destroy(gameObject);
        }
    }
}
