using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Transform menuPanel;

    private void OnEnable()
    {
        playerStats.OnDeath += ShowGameOver;
    }

    private void Start()
    {
        DefaultPanel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            menuPanel.gameObject.SetActive(true);
        else if (Input.GetKeyDown(KeyCode.Escape))
            menuPanel.gameObject.SetActive(false);
    }

    private void DefaultPanel()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnDisable()
    {

        playerStats.OnDeath -= ShowGameOver;
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void SwitchTo(GameObject _menu)
    {
        for (int i = 0; i < menuPanel.childCount; i++)
            menuPanel.GetChild(i).gameObject.SetActive(false);

        if (_menu != null)
            _menu.SetActive(true);
    }
}
