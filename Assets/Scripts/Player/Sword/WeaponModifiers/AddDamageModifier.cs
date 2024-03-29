using UnityEngine;
[CreateAssetMenu(fileName = "_AddDamageModifier", menuName = "WeaponModifiers/AddDamageModifier")]
public class AddDamageModifier : WeaponModifierSO
{
    public int AddedDamageBase, AddedDamageMultiplier;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        int newDamage = AddedDamageBase + AddedDamageMultiplier * modifierLevel;
        aPlayer.sword.SetDamage(newDamage);
        modifierDescription = "Add " + newDamage + " Damage";

    }
}
