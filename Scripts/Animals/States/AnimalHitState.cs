using UnityEngine;

[CreateAssetMenu(menuName = "Maskin/Animals/Hit")]
public class AnimalHitState : State<AnimalBehaviour>
{
    [SerializeField] private float _duration = 1f;
    private float _timer = 0f;

    private AnimalAnimations _animation;

    private bool _animationFinished;

    public override void Enter(AnimalBehaviour parent)
    {
        base.Enter(parent);
        if (_animation == null) _animation = parent.Animation;

        _runner.IsInmune = true;

        _animation.PlayHit();
    }

    public override void Tick(float deltaTime)
    {
        AnimatorStateInfo currentInfo = _animation.Animator.GetCurrentAnimatorStateInfo(0);
        if (currentInfo.length > currentInfo.normalizedTime && currentInfo.IsName("Hit"))
        {
            _animationFinished = true;
        }
    }

    public override void ChangeState()
    {
        if (_animationFinished)
        {
            _runner.SetState(typeof(AnimalIdleState));
        }
    }

    public override void FixedTick(float fixedDeltaTime) { }

    public override void Exit()
    {
        _runner.IsInmune = false;
        base.Exit();
    }
}
