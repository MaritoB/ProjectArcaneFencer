
using UnityEngine;
[CreateAssetMenu(fileName = "_LifeStealOnFirstHit", menuName = "WeaponModifiers/LifeStealOnFirstHit")]
public class LifeStealOnFirstHit : ItemModifierSO, IItemModifier
{
    public int LifeStealBase, LifeStealMultiplier;
    int CurrentLifeSteal;

    PlayerController Player;

    public void ApplyModifier(PlayerController aPlayer)
    {
        Player = aPlayer;
        Player.inventory.equipmentManager.weapon.OnFirstMeleeHit -= LifeSteal;
        Player.inventory.equipmentManager.weapon.OnFirstMeleeHit += LifeSteal;
        CurrentLifeSteal = LifeStealBase + LifeStealMultiplier * modifierLevel;
        GetDescription();
    }
    public string GetDescription()
    {
        CurrentLifeSteal = LifeStealBase + LifeStealMultiplier * modifierLevel;
        return  modifierDescription = "Your First Attack Recover " + CurrentLifeSteal + " HP per enemy hit.";
    }
    public void LifeSteal(Enemy enemy)
    {
        if(Player == null) { return; }
        Player.RecoverLife(CurrentLifeSteal);
    }

    public void RemoveModifier(PlayerController aPlayer)
    {
        Player.inventory.equipmentManager.weapon.OnFirstMeleeHit -= LifeSteal;
    }
}
