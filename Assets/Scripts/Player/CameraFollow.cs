using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Camera cam;

    public float followSpeed;

    public Vector3 offset;

    void LateUpdate()
    {
            float smoothSpeed = (GetComponent<CharacterDisplay>().character.speed * followSpeed / 2);
            //Debug.Log(smoothSpeed);
            Vector3 newCamPosition = transform.position + offset;
            Vector3 smoothedCamPosition = Vector3.Lerp(transform.position, newCamPosition, smoothSpeed);
            cam.transform.position = smoothedCamPosition;
    }

}
