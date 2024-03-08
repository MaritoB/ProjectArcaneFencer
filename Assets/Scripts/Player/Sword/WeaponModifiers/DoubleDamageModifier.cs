using UnityEngine;
[CreateAssetMenu(fileName = "DoubleDamageModifier", menuName = "WeaponModifiers/DoubleDamage")]
public class DoubleDamageModifier : WeaponModifierSO
{
    public override void ApplyModifier(SwordBase sword)
    {
        sword.AddDamage(sword.GetBaseDamage());

    }
}
