using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] private GameObject changeParticles;
    [SerializeField] private AttackSO[] meleeAttacks;
    [SerializeField] private AttackSO[] swordAttacks;

    [HideInInspector]
    [SerializeField] private GameObject weaponHolder;
    public GameObject WeaponHolder
    {
        get
        {
            return weaponHolder;
        }
        set { weaponHolder = value; }
    }

    [HideInInspector]
    [SerializeField] private bool hasSword = true;

    public bool HasSword
    {
        get
        {
            return hasSword;
        }
        set
        {
            hasSword = value;
            if (weaponHolder != null)
            {
                weaponHolder.SetActive(hasSword);
            }
        }
    }

    private HitboxHandler _hitboxHandler;

    private void Awake()
    {
        _hitboxHandler = GetComponent<HitboxHandler>();
        GetAttackByIndex(0);
    }

    private void UpdateWeaponStatus()
    {
        // Instantiate particles or something
        Instantiate(changeParticles, weaponHolder.transform.position, Quaternion.identity);
    }

    public AttackSO GetAttackByIndex(int attackIndex)
    {
        AttackSO attack = HasSword ? swordAttacks[attackIndex] : meleeAttacks[attackIndex];

        UpdateHitBoxesDamage(attack);

        return attack;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            HasSword = !HasSword;
            UpdateWeaponStatus();
        }
    }

    private void UpdateHitBoxesDamage(AttackSO attack)
    {
        _hitboxHandler.UpdateDamage(attack.Damage, attack.Knockback);
    }
}
