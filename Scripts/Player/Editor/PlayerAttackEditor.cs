using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerAttacks))]
public class PlayerAttackEditor : Editor
{
    private SerializedProperty _weaponHolder;
    private SerializedProperty _hasWeapon;

    private void OnEnable()
    {
        _weaponHolder = serializedObject.FindProperty("weaponHolder");
        _hasWeapon = serializedObject.FindProperty("hasSword");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        base.OnInspectorGUI();

        PlayerAttacks playerAttack = (PlayerAttacks)target;

        EditorGUILayout.PropertyField(_hasWeapon, new GUIContent("Has Weapon"));
        playerAttack.HasSword = _hasWeapon.boolValue;
        if (_hasWeapon.boolValue)
        {
            EditorGUILayout.PropertyField(_weaponHolder, new GUIContent("Weapon Holder"));
        }

        //apply changes. last thing to do
        serializedObject.ApplyModifiedProperties();
    }
}
