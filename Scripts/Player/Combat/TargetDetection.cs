using UnityEngine;
using UnityEngine.Events;

public class TargetDetection : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask targetMask;

    public UnityEvent<GameObject> OnTargetDetected;
    public UnityEvent<GameObject> OnTargetLost;

    private GameObject _lastDetected;

    private void Update()
    {
        int maxColliders = 3;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, radius, hitColliders, targetMask);

        if (numColliders > 0)
        {
            if (_lastDetected == null)
            {
                _lastDetected = hitColliders[0].transform.gameObject;
                OnTargetDetected?.Invoke(_lastDetected);
            }
        }
        else
        {
            if (_lastDetected != null)
            {
                OnTargetLost?.Invoke(_lastDetected);
                _lastDetected = null;
            }
        }
    }
}
