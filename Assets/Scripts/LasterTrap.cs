using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    [Header("Panels")]
    public Transform panelLeft;
    public Transform panelRight;

    [Header("Laser Beam")]
    public Transform laserBeam;
    public BoxCollider2D laserCollider;

    [Header("Beam Visual")]
    public float beamThickness = 0.3f;

    void Start() => AlignBeam();
    void Update() => SelfDestruct();

    void AlignBeam()
    {
        if (panelLeft == null || panelRight == null || laserBeam == null) return;

        // Place beam exactly between the two panels
        laserBeam.position = (panelLeft.position + panelRight.position) * 0.5f;

        // Stretch beam to fill the gap
        float distance = Vector2.Distance(panelLeft.position, panelRight.position);
        var sr = laserBeam.GetComponent<SpriteRenderer>();
        float nativeWidth = (sr != null && sr.sprite != null) ? sr.sprite.bounds.size.x : 1f;
        laserBeam.localScale = new Vector3(distance / nativeWidth, beamThickness, 1f);

        // Rotate beam to face from left to right panel
        Vector2 dir = panelRight.position - panelLeft.position;
        laserBeam.rotation = Quaternion.FromToRotation(Vector3.right, dir);

        // Resize collider to match (collider lives in local space of laserBeam)
        if (laserCollider != null)
            laserCollider.size = new Vector2(nativeWidth, 1f / beamThickness);
    }

    void SelfDestruct()
    {
        if (Camera.main != null && transform.position.x < Camera.main.transform.position.x - 20f)
            Destroy(gameObject);
    }
}