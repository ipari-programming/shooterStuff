using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Player player;

    public float smoothSpeed;

    public Vector3 offset;

    void Update()
    {
        if (player != null)
        {
            Vector3 newPosition = player.transform.position + offset;
            newPosition.z = offset.z;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
            smoothedPosition.z = offset.z;
            transform.position = smoothedPosition;
        }
    }

}
