using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ForceReceiver : MonoBehaviour
{
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void AddForce(Vector3 force)
    {
        _rb.AddForce(force, ForceMode.Impulse);
    }
}
