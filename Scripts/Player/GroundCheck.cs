using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private float _groundCheckRadius = 0.05f;

    [Tooltip("Useful for rough ground")]
    [SerializeField]
    private float _groundedOffset = -0.14f;

    [SerializeField]
    private LayerMask _collisionMask;

    public bool IsGrounded;
    // public bool IsGrounded { get; private set; }

    public bool Check()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _groundedOffset,
               transform.position.z);

        IsGrounded = Physics.CheckSphere(spherePosition, _groundCheckRadius, _collisionMask,
                QueryTriggerInteraction.Ignore);

        //Debug.Log($"GroundCheck {IsGrounded}");

        return IsGrounded;
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (IsGrounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - _groundedOffset, transform.position.z),
                _groundCheckRadius);
    }
}
