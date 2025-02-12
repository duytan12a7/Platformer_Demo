using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    private Player _player;
    [SerializeField] private SceneField[] _scenesToLoad;
    [SerializeField] private SceneField[] _scenesToUnload;

    private void Start()
    {
        _player = GameManager.Instance.Player;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == _player.gameObject)
        {
            LoadScenes();
            UnloadScenes(); 
        }
    }

    private void LoadScenes()
    {
        bool isSceneLoaded;
        for (int i = 0; i < _scenesToLoad.Length; i++)
        {
            isSceneLoaded = false;
            for (int j = 0; j < UnityEngine.SceneManagement.SceneManager.sceneCount; j++)
            {
                Scene loadedScene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(j);
                if (loadedScene.name == _scenesToLoad[i].SceneName)
                {
                    isSceneLoaded = true;
                    break;
                }
            }
            if (!isSceneLoaded)
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_scenesToLoad[i], LoadSceneMode.Additive);
        }
    }

    private void UnloadScenes()
    {
        for (int i = 0; i < _scenesToUnload.Length; i++)
        {
            for (int j = 0; j < UnityEngine.SceneManagement.SceneManager.sceneCount; j++)
            {
                Scene loadedScene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(j);
                if (loadedScene.name == _scenesToUnload[i].SceneName)
                {
                    UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(_scenesToUnload[i]);
                }
            }
        }
    }
}
