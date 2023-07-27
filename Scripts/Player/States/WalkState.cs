using UnityEngine;

[CreateAssetMenu(menuName = "Maskin/Character/Walk")]
public class WalkState : State<PlayerController>
{
    [SerializeField] private float speed = 5f;

    [Tooltip("Acceleration and deceleration")]
    [SerializeField] private float _speedChangeRate = 10.0f;

    private GroundCheck _groundCheck;
    private PlayerAnimations _animation;
    private float _animationBlend;
    private float _speed;

    public override void Enter(PlayerController parent)
    {
        base.Enter(parent);
        if (_groundCheck == null) _groundCheck = parent.GroundCheck;
        if (_animation == null) _animation = parent.Animation;

        _runner.Input.JumpEvent += OnJump;
        _runner.Animation.ResetToWalk();
    }

    public override void Tick(float deltaTime)
    {
        float inputMagnitude = _runner.Input.analogMovement ? _runner.Input.Move.magnitude : 1f;

        Move(deltaTime, inputMagnitude);

        _runner.Animation.SetSpeed(_animationBlend);
        _runner.Animation.SetMotionSpeed(inputMagnitude);
    }

    private void Move(float deltaTime, float inputMagnitude)
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = speed;

        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (_runner.Input.Move == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_runner.Controller.velocity.x, 0.0f, _runner.Controller.velocity.z).magnitude;

        float speedOffset = 0.1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                deltaTime * _speedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, deltaTime * _speedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        Vector3 playerHorizontal = _runner.PlayerTransform.right * _runner.Input.Move.x;
        Vector3 playerVertical = _runner.PlayerTransform.forward * _runner.Input.Move.y;

        Vector3 movement = (playerHorizontal + playerVertical).normalized * _speed;
        _runner.SetMomentum(movement);
    }

    public override void FixedTick(float fixedDeltaTime) { }

    public override void ChangeState()
    {
        if (_runner.Input.IsAttacking)
        {
            _runner.SetState(new AttackState(0));
        }
    }

    public override void Exit()
    {
        _runner.Input.JumpEvent -= OnJump;
        base.Exit();
    }

    private void OnJump()
    {
        if (_groundCheck.IsGrounded)
        {
            _runner.SetState(typeof(JumpState));
        }
    }
}