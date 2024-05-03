using UnityEngine;
[CreateAssetMenu(fileName = "_AddCritChanceModifier", menuName = "WeaponModifiers/AddCritChanceModifier")]
public class AddCritChanceModifier : WeaponModifierSO
{
    public int CriticalChanceBase, AddeCriticalChanceMultiplier;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        int newCriticalChance = CriticalChanceBase + AddeCriticalChanceMultiplier * modifierLevel;
        aPlayer.sword.SetCriticalChance(newCriticalChance);

    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();

        modifierDescription = "Increase Critical Chance by  " + (CriticalChanceBase + AddeCriticalChanceMultiplier * (modifierLevel + 1));
    }
}
