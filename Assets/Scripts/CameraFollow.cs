using UnityEngine;

/// <summary>
/// Attach to: Main Camera
/// Scrolls right with the player; smoothly tracks vertical position.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [Range(0.01f, 1f)] public float verticalSmoothing = 0.08f;
    public float xLead = 4f;

    void LateUpdate()
    {
        if (target == null) return;
        float targetX = target.position.x + xLead;
        float targetY = Mathf.Lerp(transform.position.y, target.position.y, verticalSmoothing);
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
