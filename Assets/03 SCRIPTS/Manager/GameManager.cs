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

    public int Level = 0;

    private void Awake()
    {
        if (instance != null || instance == this)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        LoadPlayer();
    }

    private void LoadPlayer()
    {
        if (Player != null) return;

        Player = FindObjectOfType<Player>();
        Debug.Log(transform.name + " :LoadPlayer", gameObject);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Player.IsMove = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Player.IsMove = true;
    }
}
