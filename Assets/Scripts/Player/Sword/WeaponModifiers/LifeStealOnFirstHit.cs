
using UnityEngine;
[CreateAssetMenu(fileName = "_LifeStealOnFirstHit", menuName = "WeaponModifiers/LifeStealOnFirstHit")]
public class LifeStealOnFirstHit : WeaponModifierSO
{
    public int LifeStealBase, LifeStealMultiplier;
    int CurrentLifeSteal;

    PlayerController Player;

    public override void ApplyModifier(PlayerController aPlayer)
    {
        Player = aPlayer;
        Player.sword.OnFirstMeleeHit -= LifeSteal;
        Player.sword.OnFirstMeleeHit += LifeSteal;
        CurrentLifeSteal = LifeStealBase + LifeStealMultiplier * modifierLevel;
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();

        CurrentLifeSteal = LifeStealBase + LifeStealMultiplier * modifierLevel;
        modifierDescription = modifierDescription = "Your First Strike Restore " + CurrentLifeSteal + "  Health on Hit";
    }
    public void LifeSteal(Enemy enemy)
    {
        if(Player == null) { return; }
        Player.Heal(CurrentLifeSteal);
    }
}
