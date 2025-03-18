using UnityEngine;
using Cinemachine;
using System.Collections;

public class SceneManage : MonoBehaviour
{
    public CinemachineConfiner2D cinemachineConfiner;
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 0.5f;
    public GameObject initialScenePrefab;
    public Transform player;

    private GameObject currentScene;

    private void Start()
    {
        if (initialScenePrefab != null)
            LoadScenePrefab(initialScenePrefab);
        else
            Debug.LogWarning("No ScenePrefab assigned to initialScenePrefab!");
    }

    public void LoadScenePrefab(GameObject scenePrefab)
    {
        if (currentScene != null)
            Destroy(currentScene);

        currentScene = Instantiate(scenePrefab, transform);
        currentScene.name = scenePrefab.name;
        GameManager.Instance.Level++;

        UpdateCameraBounds();
        UpdatePlayerPosition();
    }

    public void LoadSceneWithFade(GameObject scenePrefab)
    {
        StartCoroutine(FadeAndLoad(scenePrefab));
    }

    private IEnumerator FadeAndLoad(GameObject scenePrefab)
    {
        yield return StartCoroutine(Fade(1));

        LoadScenePrefab(scenePrefab);

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(Fade(0));
    }

    private void UpdateCameraBounds()
    {
        if (cinemachineConfiner == null)
        {
            Debug.LogWarning("CinemachineConfiner2D is not assigned!");
            return;
        }

        Transform cameraBounds = currentScene.transform.Find("CameraBounds");
        if (cameraBounds != null)
        {
            PolygonCollider2D boundsCollider = cameraBounds.GetComponent<PolygonCollider2D>();
            if (boundsCollider != null)
            {
                cinemachineConfiner.m_BoundingShape2D = boundsCollider;
                cinemachineConfiner.InvalidateCache();
            }
            else
                Debug.LogWarning("CameraBounds does not have a PolygonCollider2D!");
        }
        else
            Debug.LogWarning("CameraBounds not found in ScenePrefab!");
    }

    private void UpdatePlayerPosition()
    {
        if (player == null)
        {
            Debug.LogWarning("Player transform not assigned!");
            return;
        }

        Transform spawnPoint = currentScene.transform.Find("PlayerSpawnPoint");
        if (spawnPoint != null)
            player.position = spawnPoint.position;
        else
            Debug.LogWarning("PlayerSpawnPoint not found in ScenePrefab!");
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvas.alpha;
        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            yield return null;
        }

        fadeCanvas.alpha = targetAlpha;
    }
}
