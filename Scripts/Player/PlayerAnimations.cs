using UnityEngine;

public class PlayerAnimations
{
    [Tooltip("Time to pass during cross fade animations")]
    [SerializeField] private float crossFadeDuration = .1f;
    public Animator Animator => _animator;

    private readonly Animator _animator;
    private readonly Transform _transform;

    private readonly int _animIdleWalkBlend;
    private readonly int _animIDSpeed;
    private readonly int _animIDGrounded;
    private readonly int _animIDJump;
    private readonly int _animIDFreeFall;
    private readonly int _animIDMotionSpeed;

    public PlayerAnimations(Animator animator, Transform transform)
    {
        _animator = animator;
        _transform = transform;

        _animIdleWalkBlend = Animator.StringToHash("Idle Walk Run Blend");
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    public void ResetToWalk()
    {
        _animator.CrossFadeInFixedTime(_animIdleWalkBlend, crossFadeDuration);
    }

    public void SetGrounded(bool grounded)
    {
        _animator.SetBool(_animIDGrounded, grounded);
    }

    public void SetSpeed(float speed)
    {
        _animator.SetFloat(_animIDSpeed, speed);
    }

    public void SetMotionSpeed(float motionSpeed)
    {
        _animator.SetFloat(_animIDMotionSpeed, motionSpeed);
    }

    public void SetJump(bool jumping)
    {
        _animator.SetBool(_animIDJump, jumping);
    }

    public void SetFalling(bool falling)
    {
        _animator.SetBool(_animIDFreeFall, falling);
    }

    public void PlayCrossfaded(int animation, float time)
    {
        _animator.CrossFadeInFixedTime(animation, time);
    }

    public void PlayCrossfaded(string animation, float time)
    {
        PlayCrossfaded(Animator.StringToHash(animation), time);
    }
}