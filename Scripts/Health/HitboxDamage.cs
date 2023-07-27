using System.Collections.Generic;
using UnityEngine;

public class HitboxDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    public int _damage = 10;
    private float _knockback;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }

        if (alreadyCollidedWith.Contains(other)) { return; }

        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            damagable.Damage(_damage);
        }

        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * _knockback);
        }

        var allFx = other.GetComponents<IFX>();

        if (allFx != null && allFx.Length > 0)
        {
            foreach (var fx in allFx)
            {
                fx.DoFX();
            }
        }
    }

    public void SetAttack(int damage, float knockback)
    {
        _damage = damage;
        _knockback = knockback;
    }


}
