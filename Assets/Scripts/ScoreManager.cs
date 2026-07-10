using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public PlayerHealth playerHealth;

    [Header("UI Text (TextMeshPro)")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestText;

    [Header("Settings")]
    public float pointsPerUnit = 5f;

    private float _startX;
    private float _bestScore;
    private float _currentScore;
    private bool _dead;

    const string BEST_KEY = "BestRun";

    void Start()
    {
        _startX = player != null ? player.position.x : 0f;
        _bestScore = PlayerPrefs.GetFloat(BEST_KEY, 0f);
        _dead = false;
        playerHealth.OnDied += OnPlayerDied;
        RefreshUI();
    }

    void OnDisable()
    {
        if (playerHealth != null) playerHealth.OnDied -= OnPlayerDied;
    }

    void Update()
    {
        if (_dead || player == null) return;
        float distance = Mathf.Max(0f, player.position.x - _startX);
        _currentScore = distance * pointsPerUnit;
        if (scoreText != null)
            scoreText.text = $"Current Score: {Mathf.FloorToInt(_currentScore)}";
    }

    void OnPlayerDied()
    {
        _dead = true;
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
            PlayerPrefs.SetFloat(BEST_KEY, _bestScore);
            PlayerPrefs.Save();
        }
        RefreshUI();
    }

    void RefreshUI()
    {
        if (scoreText != null) scoreText.text = "Current Score: 0";
        if (bestText != null) bestText.text = $"Best Score: {Mathf.FloorToInt(_bestScore)}";
    }
}