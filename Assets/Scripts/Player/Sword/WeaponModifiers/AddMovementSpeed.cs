using UnityEngine;
[CreateAssetMenu(fileName = "_AddMovementSpeed", menuName = "WeaponModifiers/AddMovementSpeed")]
public class AddMovementSpeed : WeaponModifierSO
{
    public int AddedMovementSpeedBase, AddedMovementMultiplier;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        //int newSpeed = AddedMovementSpeedBase + AddedMovementMultiplier * modifierLevel;
        aPlayer.playerStats.movementSpeed.AddModifier(AddedMovementMultiplier * modifierLevel);
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();

        modifierDescription = "Incrase Run speed by " +( AddedMovementMultiplier )+".";
    }
}
