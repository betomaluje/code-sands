using UnityEngine;

public class PointUp : MonoBehaviour
{    
    [SerializeField] private Rigidbody target;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float maxDistance = 5;
    [SerializeField] private float kp = 100;
    [SerializeField] private float kd = 20;

    private RaycastHit hit;

    private void FixedUpdate()
    {
        //cast a ray down in the world from our current position
        if (Physics.Raycast(target.position, Vector3.down, out hit, maxDistance, groundMask))
        {
            Debug.DrawLine(target.position, target.position + Vector3.down * 10, Color.red);
            Quaternion.FromToRotation(target.rotation * Vector3.up, hit.normal).ToAngleAxis(out float angle, out Vector3 axis);
            Vector3 err = Mathf.Deg2Rad * angle * axis;
            target.AddTorque(kp * err - kd * target.angularVelocity, ForceMode.Acceleration);
        }
    }
}
