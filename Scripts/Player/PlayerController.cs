using UnityEngine;

public class PlayerController : StateRunner<PlayerController>
{
    [field: SerializeField] public GroundCheck GroundCheck { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public CharacterInput Input { get; private set; }
    [field: SerializeField] public PlayerSounds Sounds { get; private set; }
    [field: SerializeField] public PlayerAttacks Attacks { get; private set; }

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float Gravity = -15.0f;
    public float TerminalVelocity = 53.0f;

    [Header("Forces")]
    [SerializeField] private float drag = 0.1f;

    public PlayerAnimations Animation { get; private set; }
    public Transform MainCameraTransform { get; private set; }
    public Transform PlayerTransform { get; private set; }

    // for external forces
    [HideInInspector]
    public float VerticalVelocity;
    public Vector3 Movement => _momentum + Vector3.up * VerticalVelocity;

    private Vector3 dampingVelocity;
    private Vector3 _momentum = Vector3.zero;

    protected override void Awake()
    {
        MainCameraTransform = Camera.main.transform;
        PlayerTransform = transform;

        Animation = new PlayerAnimations(GetComponent<Animator>(), transform);
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();

        GroundCheck.Check();

        ApplyGravity();

        Controller.Move(Movement * Time.deltaTime);

        Animation.SetGrounded(GroundCheck.IsGrounded);

        _momentum = Vector3.SmoothDamp(_momentum, Vector3.zero, ref dampingVelocity, drag);
    }

    private void ApplyGravity()
    {
        if (GroundCheck.IsGrounded)
        {
            // stop our velocity dropping infinitely when grounded
            if (VerticalVelocity <= 0.0f)
            {
                VerticalVelocity = -2f;
            }
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (VerticalVelocity < TerminalVelocity)
        {
            VerticalVelocity += Gravity * Time.deltaTime;
        }
    }

    public void AddForce(Vector3 force)
    {
        _momentum += force;
    }

    public void SetMomentum(Vector3 momentum)
    {
        _momentum = momentum;
    }

    public void Rotate(Quaternion rotation)
    {
        // rotate to face input direction relative to camera position
        PlayerTransform.rotation = rotation;
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        Sounds.PlayFootStep(animationEvent, transform.TransformPoint(Controller.center));
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        Sounds.PlayOnLand(animationEvent, transform.TransformPoint(Controller.center));
    }

    public void RotateTowardsMouse(Vector3 mouseDirection)
    {
        PlayerTransform.forward = mouseDirection;
    }
}
