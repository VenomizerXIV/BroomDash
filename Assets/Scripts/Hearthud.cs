using UnityEngine;
using UnityEngine.UI;


public class HeartHUD : MonoBehaviour
{
    [Header("References")]
    public PlayerHealth playerHealth;

    [Header("Heart Images (left to right)")]
    public Image heart1;
    public Image heart2;
    public Image heart3;

    [Header("Sprites")]
    public Sprite fullHeart;
    public Sprite emptyHeart;   // if you only have one sprite, leave this empty

    private Image[] _hearts;

    void Awake()
    {
        _hearts = new Image[] { heart1, heart2, heart3 };
    }

    void Update()
    {
        if (playerHealth == null) return;

        for (int i = 0; i < _hearts.Length; i++)
        {
            if (_hearts[i] == null) continue;

            bool filled = i < playerHealth.CurrentHP;

            if (emptyHeart != null)
            {
                // swap between full and empty sprite
                _hearts[i].sprite = filled ? fullHeart : emptyHeart;
                _hearts[i].color = Color.white;
            }
            else
            {
                // no empty sprite — dim the heart instead
                _hearts[i].sprite = fullHeart;
                _hearts[i].color = filled ? Color.white : new Color(0.15f, 0.15f, 0.15f, 0.4f);
            }
        }
    }
}