using UnityEngine;

[CreateAssetMenu(menuName = "Maskin/Animals/Idle")]
public class AnimalIdleState : State<AnimalBehaviour>
{
    [SerializeField] private float countDownTime = 5f;
    [SerializeField] private bool shouldWander = true;
    [SerializeField, Range(0f, 1f)] private float wanderChance = .5f;

    private AnimalIdleType _idleType;
    private AnimalAnimations _animation;
    private float _timeRemaining;

    public override void Enter(AnimalBehaviour parent)
    {
        base.Enter(parent);
        if (_animation == null) _animation = parent.Animation;

        _timeRemaining = countDownTime;
        _idleType = (AnimalIdleType)Random.Range(0, System.Enum.GetValues(typeof(AnimalIdleType)).Length);

        switch (_idleType)
        {
            case AnimalIdleType.TypeA:
                _runner.Animation.PlayIdleA();
                break;
            case AnimalIdleType.TypeB:
                _runner.Animation.PlayIdleB();
                break;
            case AnimalIdleType.TypeC:
                _runner.Animation.PlayIdleC();
                break;
            case AnimalIdleType.Sit:
                _runner.Animation.PlaySit();
                break;
            default:
                _runner.Animation.PlayIdleA();
                break;
        }
    }

    public override void FixedTick(float fixedDeltaTime) { }

    public override void Tick(float deltaTime)
    {
        _timeRemaining -= deltaTime;
    }

    public override void ChangeState()
    {
        if (_timeRemaining <= 0)
        {
            if (shouldWander)
            {
                // check chance
                float chance = Random.Range(0f, 1f);
                if (wanderChance >= chance)
                {
                    _runner.SetState(typeof(AnimalWalkState));
                }
                else
                {
                    _runner.SetState(typeof(AnimalIdleState));
                }
            }
            else
            {
                _runner.SetState(typeof(AnimalIdleState));
            }
        }
    }
}

[System.Serializable]
public enum AnimalIdleType
{
    TypeA,
    TypeB,
    TypeC,
    Sit
}