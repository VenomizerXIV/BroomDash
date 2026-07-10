using UnityEngine;
using UnityEngine.UI;


public class BoostGaugeUI : MonoBehaviour
{
    [Header("References")]
    public JetpackController player;
    public Image fillBar;

    [Header("Colors")]
    public Color fullColor = Color.cyan;
    public Color boostColor = Color.yellow;
    public Color emptyColor = Color.red;

    void Update()
    {
        if (player == null || fillBar == null) return;
        float t = player.boostFuel / player.maxBoostFuel;
        fillBar.fillAmount = t;
        fillBar.color = t < 0.2f ? emptyColor : (player.isBoosting ? boostColor : fullColor);
    }
}
