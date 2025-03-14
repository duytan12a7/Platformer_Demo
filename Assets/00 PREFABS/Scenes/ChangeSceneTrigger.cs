using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    public GameObject nextScenePrefab;
    public ParticleSystem glowEffect;

    private void OnEnable()
    {
        glowEffect.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManage sceneLoader = FindObjectOfType<SceneManage>();
            if (sceneLoader != null)
            {
                sceneLoader.LoadSceneWithFade(nextScenePrefab);
            }
        }
    }
}