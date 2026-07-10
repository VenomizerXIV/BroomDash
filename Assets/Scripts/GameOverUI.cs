using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour
{
    [Header("References")]
    public GameObject gameOverPanel;
    public PlayerHealth playerHealth;

    void OnEnable() => playerHealth.OnDied += OnPlayerDied;
    void OnDisable() => playerHealth.OnDied -= OnPlayerDied;
    void Start() => gameOverPanel.SetActive(false);

    void OnPlayerDied() => StartCoroutine(ShowAfterDelay());

    IEnumerator ShowAfterDelay()
    {
        
        yield return new WaitForSecondsRealtime(1f);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // Called by Play Again button
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}