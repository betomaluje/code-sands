using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInput : MonoBehaviour, PlayerControls.IMenuActions
{
    public Vector2 Look { get; private set; }

    public bool IsSelectPressed { get; private set; }
    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.Menu.SetCallbacks(this);

        _playerControls.Menu.Enable();
    }

    private void OnDestroy()
    {
        _playerControls.Menu.Disable();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsSelectPressed = true;
        }
        else if (context.canceled)
        {
            IsSelectPressed = false;
        }
    }
}
