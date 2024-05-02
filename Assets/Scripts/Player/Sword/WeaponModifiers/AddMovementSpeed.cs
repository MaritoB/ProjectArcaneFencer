using UnityEngine;
[CreateAssetMenu(fileName = "_AddMovementSpeed", menuName = "WeaponModifiers/AddMovementSpeed")]
public class AddMovementSpeed : WeaponModifierSO
{
    public int AddedMovementSpeedBase, AddedMovementMultiplier;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        int newSpeed = AddedMovementSpeedBase + AddedMovementMultiplier * modifierLevel;
        aPlayer.playerData.MovementSpeed = newSpeed;
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();

        modifierDescription = "Incrase Run speed by " +( AddedMovementMultiplier )+".";
    }
}
