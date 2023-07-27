using UnityEngine;

public class AnimalAnimations
{
    [Tooltip("Time to pass during cross fade animations")]
    [SerializeField] private float crossFadeDuration = .1f;
    public Animator Animator => _animator;

    private readonly Animator _animator;
    private readonly Transform _transform;

    private readonly int _animWalk;
    private readonly int _animIdle_A;
    private readonly int _animIdle_B;
    private readonly int _animIdle_C;
    private readonly int _animSit;
    private readonly int _animEat;
    private readonly int _animHit;
    private readonly int _animDie;

    public AnimalAnimations(Animator animator, Transform transform)
    {
        _animator = animator;
        _transform = transform;

        _animWalk = Animator.StringToHash("Walk");
        _animIdle_A = Animator.StringToHash("Idle_A");
        _animIdle_B = Animator.StringToHash("Idle_B");
        _animIdle_C = Animator.StringToHash("Idle_C");
        _animSit = Animator.StringToHash("Sit");
        _animEat = Animator.StringToHash("Eat");
        _animHit = Animator.StringToHash("Hit");
        _animDie = Animator.StringToHash("Death");
    }

    private void PlayCrossFade(int animation)
    {
        _animator.CrossFadeInFixedTime(animation, crossFadeDuration);
    }

    public void PlayWalk()
    {
        PlayCrossFade(_animWalk);
    }

    public void PlayIdleA()
    {
        PlayCrossFade(_animIdle_A);
    }

    public void PlayIdleB()
    {
        PlayCrossFade(_animIdle_B);
    }

    public void PlayIdleC()
    {
        PlayCrossFade(_animIdle_C);
    }

    public void PlaySit()
    {
        PlayCrossFade(_animSit);
    }

    public void PlayEat()
    {
        PlayCrossFade(_animEat);
    }

    public void PlayHit()
    {
        PlayCrossFade(_animHit);
    }

    public void PlayDie()
    {
        PlayCrossFade(_animDie);
    }
}
