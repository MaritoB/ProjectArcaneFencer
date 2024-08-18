using UnityEngine;
[CreateAssetMenu(fileName = "_AddDamageModifier", menuName = "WeaponModifiers/AddDamageModifier")]
public class AddDamageModifier : WeaponModifierSO
{
    public int AddedDamageBase, AddedDamageMultiplier;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        int newDamage = AddedDamageBase + AddedDamageMultiplier * modifierLevel;
        aPlayer.inventory.equipmentManager.weapon.SetDamage(newDamage);

    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();

        modifierDescription = "Add " + (AddedDamageBase + AddedDamageMultiplier *( modifierLevel + 1 )) + " Damage";
    }
}
