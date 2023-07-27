using UnityEngine;

[CreateAssetMenu(menuName = "Maskin/Attacks")]
public class AttackSO : ScriptableObject
{
    [field: SerializeField] public string attackName;
    [field: SerializeField] public float TransitionDuration { get; private set; } = .1f;
    [field: SerializeField, Tooltip("Which index is the next combo. -1 means it's the last one or has not next one")]
    public int ComboStateIndex { get; private set; } = -1;
    [field: SerializeField] public float ComboAttackTime { get; private set; }
    [field: SerializeField] public float ForceTime { get; private set; }
    [field: SerializeField] public float Force { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float Knockback { get; private set; }

}
