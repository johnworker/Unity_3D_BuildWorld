using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the target (scene object) to follow

    private void Update()
    {
        if (target != null)
        {
            // Update the position of the camera to match the target's position
            transform.position = new Vector3(-2f, target.position.y, transform.position.z);
        }
    }
}