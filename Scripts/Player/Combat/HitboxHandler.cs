using UnityEngine;

public class HitboxHandler : MonoBehaviour
{
    [SerializeField] private HitboxDamage hitBoxRight;
    [SerializeField] private HitboxDamage hitBoxLeft;
    [SerializeField] private HitboxDamage hitBoxKick;
    [SerializeField] private HitboxDamage hitboxSword;

    public void UpdateDamage(int damage, float knockback)
    {
        hitBoxRight.SetAttack(damage, knockback);
        hitBoxLeft.SetAttack(damage, knockback);
        hitBoxKick.SetAttack(damage, knockback);
        hitboxSword.SetAttack(damage, knockback);
    }

    public void EnableRightHitbox()
    {
        hitBoxRight.gameObject.SetActive(true);
    }

    public void DisableRightHitbox()
    {
        hitBoxRight.gameObject.SetActive(false);
    }

    public void EnableLeftHitbox()
    {
        hitBoxLeft.gameObject.SetActive(true);
    }

    public void DisableLeftHitbox()
    {
        hitBoxLeft.gameObject.SetActive(false);
    }

    public void EnableKickHitbox()
    {
        hitBoxKick.gameObject.SetActive(true);
    }

    public void DisableKickHitbox()
    {
        hitBoxKick.gameObject.SetActive(false);
    }

    public void EnableSwordHitbox()
    {
        hitboxSword.gameObject.SetActive(true);
    }

    public void DisableSwordHitbox()
    {
        hitboxSword.gameObject.SetActive(false);
    }
}
