
using UnityEngine;
[CreateAssetMenu(fileName = "_ChainLightningOnSecondStrikeModifier", menuName = "WeaponModifiers/ChainLightningOnSecondStrikeModifier")]
public class ChainLightningOnSecondStrikeModifier : WeaponModifierSO
{ 
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int CurrentTriggerChance;

    PlayerController mPlayer;

    public override void ApplyModifier(PlayerController aPlayer)
    {
        mPlayer = aPlayer;
        aPlayer.inventory.equipmentManager.weapon.OnSecondMeleeHit -= TryCastChainLightning;
        aPlayer.inventory.equipmentManager.weapon.OnSecondMeleeHit += TryCastChainLightning;
        CurrentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
        mPlayer.mSkillManager.LevelUpChainLightning(modifierLevel);
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();

        modifierDescription = "+1 to Chain Lightning & " + CurrentTriggerChance + "% chance to cast it hitting enemies with your Second Attack.";
    }
    public void TryCastChainLightning(Enemy enemy)
    {
        int number = Random.Range(0, 100);
        if (number < CurrentTriggerChance)
        {
            Vector3 Direction = (enemy.transform.position - mPlayer.transform.position).normalized;
            mPlayer.mSkillManager.UseChainLightningSkill(Direction);
        }

    }
}
