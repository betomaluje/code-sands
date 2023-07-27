using UnityEngine;

[CreateAssetMenu(menuName = "Maskin/Character/Attack")]
public class AttackState : State<PlayerController>
{
    private GroundCheck _groundCheck;
    private PlayerAnimations _animation;
    private AttackSO _currentAttack;

    private float _previousFrameTime;
    private bool _alreadyAppliedForce;
    private bool _doneAttacking;

    private int _attackIndex;

    public AttackState(int attackIndex)
    {
        _attackIndex = attackIndex;
    }

    public override void Enter(PlayerController parent)
    {
        base.Enter(parent);
        if (_groundCheck == null) _groundCheck = parent.GroundCheck;
        if (_animation == null) _animation = parent.Animation;

        _currentAttack = parent.Attacks.GetAttackByIndex(_attackIndex);

        _animation.PlayCrossfaded(_currentAttack.attackName, _currentAttack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        float normalizedTime = GetNormalizedTime(_runner.Animation.Animator, "Attack");

        if (normalizedTime >= _previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= _currentAttack.ForceTime)
            {
                TryApplyForce();
            }

            if (_runner.Input.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            _doneAttacking = true;
        }

        _previousFrameTime = normalizedTime;

    }

    private void TryComboAttack(float normalizedTime)
    {
        if (_currentAttack.ComboStateIndex == -1) { return; }

        if (normalizedTime < _currentAttack.ComboAttackTime) { return; }

        _runner.SetState(new AttackState(_currentAttack.ComboStateIndex));
    }

    private void TryApplyForce()
    {
        if (_alreadyAppliedForce) { return; }

        _runner.AddForce(_runner.transform.forward * _currentAttack.Force);

        _alreadyAppliedForce = true;
    }

    public override void ChangeState()
    {
        if (_doneAttacking)
        {
            _runner.SetState(typeof(WalkState));
        }
    }

    public override void FixedTick(float fixedDeltaTime) { }

    public override void Exit()
    {
        _runner.Animation.SetSpeed(0.0f);
        base.Exit();
    }
}
