using UnityEngine;


public class BackgroundScroller : MonoBehaviour
{
    [Header("Backgrounds — assign all 5 sprites in order")]
    public Sprite[] backgrounds;

    [Header("References")]
    public Transform cameraTransform;

   

    private SpriteRenderer[] _panels;
    private float _panelWidth;
    private int _nextIndex;
    private float _anchorY;
    private float _anchorZ;

    void Start()
    {
        _panels = GetComponentsInChildren<SpriteRenderer>();

        if (_panels.Length == 0)
        {
            Debug.LogError("BackgroundScroller: No SpriteRenderer children found.");
            return;
        }
        if (backgrounds == null || backgrounds.Length == 0)
        {
            Debug.LogError("BackgroundScroller: No sprites assigned.");
            return;
        }

        // Use this GameObject's Y and Z as the anchor — set them in the Scene view
        _anchorY = transform.position.y;
        _anchorZ = transform.position.z;

        // Assign sprites
        for (int i = 0; i < _panels.Length; i++)
        {
            _panels[i].sprite = backgrounds[_nextIndex % backgrounds.Length];
            _panels[i].sortingOrder = -10;
            _nextIndex++;
        }

        // Detect width
        _panelWidth = _panels[0].bounds.size.x;
        if (_panelWidth <= 0f)
        {
            Debug.LogWarning("BackgroundScroller: Width is 0. Check sprite PPU. Using fallback 20.");
            _panelWidth = 20f;
        }

        // Start panels from this GameObject's X position
        float startX = transform.position.x;
        for (int i = 0; i < _panels.Length; i++)
            _panels[i].transform.position = new Vector3(startX + _panelWidth * i, _anchorY, _anchorZ);
    }

    void LateUpdate()
    {
        if (_panels == null || cameraTransform == null) return;

        float cullX = cameraTransform.position.x - _panelWidth;

        foreach (var panel in _panels)
        {
            if (panel.transform.position.x + _panelWidth / 2f < cullX)
            {
                float newX = GetRightmostX() + _panelWidth;
                panel.transform.position = new Vector3(newX, _anchorY, _anchorZ);
                panel.sprite = backgrounds[_nextIndex % backgrounds.Length];
                _nextIndex++;
            }
        }
    }

    float GetRightmostX()
    {
        float max = float.MinValue;
        foreach (var p in _panels) max = Mathf.Max(max, p.transform.position.x);
        return max;
    }
}