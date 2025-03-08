using System;
using UnityEngine;

public class DespawnAllEnemy : MonoBehaviour
{
    public static event Action OnDespawnAllEnemy;
    // private Player _player;

    private void Start()
    {
        // _player = GameManager.Instance.Player;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        OnDespawnAllEnemy?.Invoke();
    }
}
