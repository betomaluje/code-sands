using BerserkTools.Utils;
using Cinemachine;
using UnityEngine;

public class NPCCamera : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float radius = 5f;
    [SerializeField] private SphereCollider sphereCollider;

    [Header("Face Cameras Setup")]
    [SerializeField] private CinemachineVirtualCamera faceCamera;
    [SerializeField] private Transform playerTransform;

    private CustomDialogRenderer _renderer;
    private CinemachineVirtualCamera _targetCamera;

    private void Start()
    {
        _renderer = FindObjectOfType<CustomDialogRenderer>();
    }

    private void Update()
    {
        sphereCollider.radius = radius;
    }

    private void ActivateCamera(Transform target)
    {
        var head = target.GetComponentInChildren<NPCDialog>();
        if (head != null)
        {
            _renderer.otherTarget = head.CameraTarget;

            PositionCameraInBetween(head.CameraTarget);
        }

        _targetCamera = target.GetComponentInChildren<CinemachineVirtualCamera>();
        if (_targetCamera != null)
        {
            _targetCamera.enabled = true;
        }
    }

    private void PositionCameraInBetween(Transform npcTransform)
    {
        float originalYPos = playerTransform.position.y;
        Vector3 middlePoint = (playerTransform.position + npcTransform.position) / 2f;
        middlePoint.y = originalYPos;

        faceCamera.transform.position = middlePoint;
    }

    private void DeactivateCamera(Transform target)
    {
        _renderer.otherTarget = null;

        if (_targetCamera == null) return;

        _targetCamera.enabled = false;
        _targetCamera = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetMask.LayerMatchesObject(other.gameObject))
        {
            ActivateCamera(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (targetMask.LayerMatchesObject(other.gameObject))
        {
            DeactivateCamera(other.transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (sphereCollider == null) return;
        Gizmos.color = new Color(0, 0, 0.5f, 0.3f);
        Gizmos.DrawSphere(transform.position + sphereCollider.center, radius);
    }
}
