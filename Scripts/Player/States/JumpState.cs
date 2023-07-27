using UnityEngine;

[CreateAssetMenu(menuName = "Maskin/Character/Jump")]
public class JumpState : State<PlayerController>
{
    [Tooltip("The height the player can jump")]
    public float JumpHeight = 1.2f;

    [Header("Timeouts")]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.50f;

    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float FallTimeout = 0.15f;

    private GroundCheck _groundCheck;
    private PlayerAnimations _animation;

    private bool _leftTheGround;
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;
    private float _terminalVelocity = 53.0f;

    // to store the previous momentum
    private Vector3 _momentum;

    public override void Enter(PlayerController parent)
    {
        base.Enter(parent);
        if (_groundCheck == null) _groundCheck = parent.GroundCheck;
        if (_animation == null) _animation = parent.Animation;

        _leftTheGround = false;

        _momentum = _runner.Controller.velocity;
        _momentum.y = 0f;

        // reset our timeouts on start
        _jumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;

        Jump();
    }

    public override void Tick(float deltaTime)
    {
        if (!_groundCheck.IsGrounded)
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= deltaTime;
            }
            else
            {
                // falling state
                _runner.Animation.SetFalling(true);
            }

            _leftTheGround = true;
        }

        // move the player
        _runner.SetMomentum(_momentum);
    }

    private void Jump()
    {
        // the square root of H * -2 * G = how much velocity needed to reach desired height
        _runner.VerticalVelocity = Mathf.Sqrt(JumpHeight * -2f * _runner.Gravity);
        _runner.Animation.SetJump(true);
    }

    public override void FixedTick(float fixedDeltaTime) { }

    public override void ChangeState()
    {
        if (_leftTheGround && _groundCheck.IsGrounded)
        {
            _runner.SetState(typeof(WalkState));
        }
    }
    public override void Exit()
    {
        _runner.Animation.SetJump(false);
        _runner.Animation.SetFalling(false);
        base.Exit();
    }
}