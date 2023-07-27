using UnityEngine;

public class SwingObject : MonoBehaviour
{
    [SerializeField] private float maxRotation;
    [SerializeField] private float speed = 2.0f;

    private Quaternion _startPos;

    private void Start()
    {
        _startPos = transform.rotation;
    }

    private void LateUpdate()
    {
        Quaternion newRotation = Quaternion.Euler(0, 0, maxRotation * Mathf.Sin(Time.time * speed));
        newRotation.x = _startPos.x;
        newRotation.y = _startPos.y;
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime);
    }
}
