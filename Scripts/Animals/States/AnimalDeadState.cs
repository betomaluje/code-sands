using UnityEngine;

[CreateAssetMenu(menuName = "Maskin/Animals/Dead")]
public class AnimalDeadState : State<AnimalBehaviour>
{
    private AnimalAnimations _animation;

    public override void Enter(AnimalBehaviour parent)
    {
        base.Enter(parent);
        if (_animation == null) _animation = parent.Animation;

        _runner.IsInmune = true;
        _animation.PlayDie();
    }

    public override void ChangeState() { }

    public override void FixedTick(float fixedDeltaTime) { }

    public override void Tick(float deltaTime)
    {
        AnimatorStateInfo animStateInfo = _runner.Animation.Animator.GetCurrentAnimatorStateInfo(0);
        float animationTime = animStateInfo.normalizedTime;

        if (animationTime >= 1.0f)
        {
            _runner.DestroyWhenDead();
        }
    }
}
