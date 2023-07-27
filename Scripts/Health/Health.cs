using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] private int maxHealth = 100;

    private int health;

    public Action<int, int> OnTakeDamage;
    public Action OnDie;

    public bool IsDead => health == 0;

    private void Start()
    {
        health = maxHealth;
    }

    public void Damage(int amount)
    {
        if (IsDead) { return; }

        health = Mathf.Max(health - amount, 0);

        OnTakeDamage?.Invoke(amount, health);

        if (IsDead)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDie?.Invoke();
    }
}
