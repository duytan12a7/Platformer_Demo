using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [field: SerializeField]
    public Player Player { get; private set; }

    public Transform PlayerSpawn { get; private set; }

    private void Awake()
    {
        if (instance != null || instance == this)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        LoadPlayer();
    }

    private void LoadPlayer()
    {
        if (Player != null) return;

        Player = FindObjectOfType<Player>();
        Debug.Log(transform.name + " :LoadPlayer", gameObject);
    }
}
