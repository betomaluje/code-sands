using UnityEngine;

public class AnimalBehaviour : StateRunner<AnimalBehaviour>
{
    public AnimalAnimations Animation { get; private set; }

    public Transform AnimalTransform { get; private set; }

    public Health Health { get; private set; }

    public bool IsInmune;

    private Rigidbody _rb;

    protected override void Awake()
    {
        AnimalTransform = transform;
        Animation = new AnimalAnimations(GetComponentInChildren<Animator>(), transform);
        Health = GetComponent<Health>();
        _rb = GetComponent<Rigidbody>();
        base.Awake();
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDeath;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDeath;
    }

    private void HandleTakeDamage(int damage, int health)
    {
        SetState(new AnimalHitState());
    }

    private void HandleDeath()
    {
        SetState(new AnimalDeadState());
    }

    public void LookAt(Vector3 lookAt)
    {
        lookAt.y = AnimalTransform.localPosition.y;
        AnimalTransform.LookAt(lookAt);
    }

    public void Move(Vector3 destination, float speed)
    {
        AnimalTransform.position = Vector3.MoveTowards(AnimalTransform.position, destination, speed * Time.deltaTime);
    }

    public void DestroyWhenDead()
    {
        Destroy(this);
    }
}
