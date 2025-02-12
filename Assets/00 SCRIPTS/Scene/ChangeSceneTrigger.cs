using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    public GameObject nextScenePrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager sceneLoader = FindObjectOfType<SceneManager>();
            if (sceneLoader != null)
            {
                sceneLoader.LoadSceneWithFade(nextScenePrefab);
            }
        }
    }
}