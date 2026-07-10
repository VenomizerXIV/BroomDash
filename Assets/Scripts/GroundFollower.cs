using UnityEngine;
public class GroundFollower : MonoBehaviour
{
    public Transform cameraTransform;

    void LateUpdate()
    {
        if (cameraTransform == null) return;
        transform.position = new Vector3(
            cameraTransform.position.x,
            transform.position.y,
            transform.position.z
        );
    }
}