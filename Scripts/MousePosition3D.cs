using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MousePosition3D : MonoBehaviour
{
    [SerializeField] private Transform crosshair;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private bool ignoreHeight;
    [SerializeField] private LayerMask terrainMask;
    [SerializeField] private float speed;

    private Camera mainCamera;
    private bool isEnabled;

    public UnityEvent<Vector3> OnMouseDirection;

    private void Start()
    {
        mainCamera = Camera.main;
        SetEnabled(true);
    }

    private void Update()
    {
        if (!isEnabled) return;

        Aim();
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // we move the crosshair transform to show where are we aiming
            crosshair.position = Vector3.Lerp(crosshair.position, position, speed * Time.deltaTime);

            // Calculate the direction
            var direction = (position - playerTransform.position);

            if (ignoreHeight)
            {
                // Ignore the height difference.
                direction.y = 0;
            }

            // Make the transform look in the direction.
            OnMouseDirection?.Invoke(direction);
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, terrainMask))
        {
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }

    public void SetEnabled(bool enabled)
    {
        isEnabled = enabled;

        crosshair.gameObject.SetActive(isEnabled);
    }
}
