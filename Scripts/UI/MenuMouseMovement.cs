using UnityEngine;
using UnityEngine.InputSystem;

public class MenuMouseMovement : MonoBehaviour
{
    [SerializeField] private Transform targetToMove;
    [SerializeField] private float speed;
    [SerializeField] private float zPosition = 5f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update() 
    {
        var ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 position = hitInfo.point;
            position.z = zPosition;
            // we move the crosshair transform to show where are we aiming
            targetToMove.position = Vector3.Lerp(targetToMove.position, position, speed * Time.deltaTime);
        }
    }
}
