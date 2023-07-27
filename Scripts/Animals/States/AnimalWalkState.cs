using UnityEngine;

[CreateAssetMenu(menuName = "Maskin/Animals/Wander", fileName = "New Wander State")]
public class AnimalWalkState : State<AnimalBehaviour>
{
    [SerializeField] private float movementRadius = 5f;
    [SerializeField] private float arrivalDistance = 1f;
    [SerializeField] private float speed = 10f;

    private Vector3 _targetPosition;
    private AnimalAnimations _animation;

    public override void Enter(AnimalBehaviour parent)
    {
        base.Enter(parent);
        if (_animation == null) _animation = parent.Animation;

        _targetPosition = GetRandomPosition();
        _animation.PlayWalk();
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 myPosition = new Vector2(_runner.AnimalTransform.position.x, _runner.AnimalTransform.position.z);
        Vector2 randomPoint = myPosition + Random.insideUnitCircle * movementRadius;
        return new Vector3(randomPoint.x, 0, randomPoint.y);
    }

    public override void Tick(float deltaTime)
    {
        _runner.LookAt(_targetPosition);
        _runner.Move(_targetPosition, speed);
    }

    public override void ChangeState()
    {
        float dist = Vector3.Distance(_targetPosition, _runner.AnimalTransform.position);
        if (dist < arrivalDistance)
        {
            _runner.SetState(typeof(AnimalIdleState));
        }
    }

    public override void FixedTick(float fixedDeltaTime) { }
}
