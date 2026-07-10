using UnityEngine;


public class PauseManager : MonoBehaviour
{
    [Header("References")]
    public GameObject pausePanel;
    public PlayerHealth playerHealth;

    [Header("Optional — hide these buttons while paused")]
    public GameObject stopButton;

    private bool _paused;

    void Start() => pausePanel.SetActive(false);

    public void Pause()
    {
        // Block pause if player is already dead
        if (playerHealth != null && !playerHealth.IsAlive) return;

        _paused = true;
        pausePanel.SetActive(true);
        if (stopButton != null) stopButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        _paused = false;
        pausePanel.SetActive(false);
        if (stopButton != null) stopButton.SetActive(true);
        Time.timeScale = 1f;
    }
}