using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.forward,
            cam.rotation * Vector3.up);
    }
}
