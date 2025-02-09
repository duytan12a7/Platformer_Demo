using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnAllEnemy : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GameManager.Instance.Player;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == _player.gameObject)
            SpawnEnemy.Instance.ResetAllSpawns();
    }
}
