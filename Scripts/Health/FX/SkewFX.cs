using System.Collections;
using UnityEngine;

public class SkewFX : MonoBehaviour, IFX
{
    [Tooltip("Skew factor to switch to")]
    [SerializeField] private float skewFactor = .2f;

    [Tooltip("Duration of the flash.")]
    [SerializeField] private float duration;

    private Vector3 _originalScale;

    // The currently running coroutine.
    private Coroutine _skewRoutine;

    private void Start()
    {
        _originalScale = transform.localScale;
    }

    public void DoFX()
    {
        // If the _skewRoutine is not null, then it is currently running.
        if (_skewRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple SkewRoutines the same time would cause bugs.
            StopCoroutine(_skewRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        _skewRoutine = StartCoroutine(SkewRoutine());
    }

    private IEnumerator SkewRoutine()
    {
        // Swap to the skewFactor.
        float x = transform.localScale.x + Random.Range(0f, skewFactor);
        float y = transform.localScale.y + Random.Range(0f, skewFactor);
        float z = transform.localScale.z + Random.Range(0f, skewFactor);

        transform.localScale = new Vector3(x, y, z);

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(duration);

        // After the pause, swap back to the original material.
        transform.localScale = _originalScale;

        // Set the routine to null, signaling that it's finished.
        _skewRoutine = null;
    }
}
