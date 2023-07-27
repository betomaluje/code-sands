using BerserkPixel.Prata;
using UnityEngine;

public class PlayerDialogInteractor : MonoBehaviour
{
    [SerializeField] private GameObject interactCanvas;

    [SerializeField] private CharacterInput playerControls;

    [SerializeField] private GameObject cameraContainer;

    private Interaction _interaction;

    private void Start()
    {
        playerControls.InteractEvent += OnInteract;
    }

    private void OnDestroy()
    {
        playerControls.InteractEvent -= OnInteract;
    }

    private void OnInteract()
    {
        if (_interaction != null)
        {
            Interact();
        }
    }

    public void ReadyForInteraction(Interaction newInteraction)
    {
        _interaction = newInteraction;
    }

    public void Interact()
    {
        if (_interaction != null)
        {
            DialogManager.Instance.Talk(_interaction);
        }
    }

    public void ShowCanvas()
    {
        if (playerControls.cursorLocked)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        interactCanvas.SetActive(true);
        cameraContainer.SetActive(true);
    }

    public void HideCanvas()
    {
        if (playerControls.cursorLocked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        interactCanvas.SetActive(false);
        cameraContainer.SetActive(false);
    }
}
