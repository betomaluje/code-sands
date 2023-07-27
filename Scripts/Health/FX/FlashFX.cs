using System.Collections;
using UnityEngine;

public class FlashFX : MonoBehaviour, IFX
{
    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("Duration of the flash.")]
    [SerializeField] private float duration;
    [SerializeField] private int numberOfFlashes = 1;

    private Renderer _renderer;
    // The material that was in use, when the script started.
    private Material _originalMaterial;

    // The currently running coroutine.
    private Coroutine _flashRoutine;

    private void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();

        _originalMaterial = _renderer.material;
    }

    public void DoFX()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (_flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(_flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        _flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        float durationPerFlash = duration / numberOfFlashes;
        // we divide by 2 since we need to turn to flash and back to original with a pause
        WaitForSeconds waitingTime = new WaitForSeconds(durationPerFlash / 2);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            // Swap to the flashMaterial.
            _renderer.material = flashMaterial;

            yield return waitingTime;

            // After the pause, swap back to the original material.
            _renderer.material = _originalMaterial;

            // so we show the original material for the same amount of time
            yield return waitingTime;
        }

        _renderer.material = _originalMaterial;

        // Set the routine to null, signaling that it's finished.
        _flashRoutine = null;
    }
}
