using UnityEngine;
[CreateAssetMenu(fileName = "_FireballOnFirstPerformedModifier", menuName = "WeaponModifiers/FireballOnFirstPerformedModifier")]
public class FireballOnFirstPerformedModifier : WeaponModifierSO
{
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int currentTriggerChance;
    PlayerController Player;

    public override void ApplyModifier(PlayerController aPlayer)
    {
        Player = aPlayer;
        Player.mSkillManager.LevelUpFireball(modifierLevel);
        aPlayer.sword.OnSecondMeleePerformed -= TryCastFireBall;
        aPlayer.sword.OnSecondMeleePerformed += TryCastFireBall;
        currentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();
        modifierDescription = "+1 to Fireball & " + (TriggerChanceBase + TriggerChanceMultiplier * (modifierLevel + 1)) + "% chance to cast it on First Attack. ";
    }
    public void TryCastFireBall()
    {
        int number = Random.Range(0, 100);
        if (number < currentTriggerChance)
        {
            Player.mSkillManager.UseIceNova();
        }

    }

}
