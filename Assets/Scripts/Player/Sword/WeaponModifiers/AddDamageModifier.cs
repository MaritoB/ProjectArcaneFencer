using UnityEngine;
[CreateAssetMenu(fileName = "_AddDamageModifier", menuName = "WeaponModifiers/AddDamageModifier")]
public class AddDamageModifier : WeaponModifierSO
{
    public int AddedDamageBase, AddedDamageMultiplier;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        int newDamage = AddedDamageBase + AddedDamageMultiplier * modifierLevel;
        aPlayer.sword.SetDamage(newDamage);

    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();

        modifierDescription = "Add " + (AddedDamageBase + AddedDamageMultiplier * modifierLevel) + " Damage";
    }
}
