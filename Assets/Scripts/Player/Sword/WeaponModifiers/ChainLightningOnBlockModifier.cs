using UnityEngine;

[CreateAssetMenu(fileName = "_ChainLightningOnBlockModifier", menuName = "WeaponModifiers/ChainLightningOnBlockModifier")]
public class ChainLightningOnBlockModifier : WeaponModifierSO
{
    PlayerController mPlayer; 
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int CurrentTriggerChance;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        mPlayer = aPlayer;
        CurrentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
        aPlayer.OnBlockPerformed -= CastProjectile;
        aPlayer.OnBlockPerformed += CastProjectile;
        mPlayer.mSkillManager.LevelUpChainLightning(modifierLevel);
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        modifierDescription ="+1 to Chain Lightning & "+(TriggerChanceBase + TriggerChanceMultiplier * ( modifierLevel +1 )) + "% chance to cast it when blocking melee damage.";
    }
    public void CastProjectile(Enemy aEnemy)
    {
        int number = Random.Range(0, 100);
        if (number < CurrentTriggerChance)
        {
            Vector3 direction = (mPlayer.transform.position - aEnemy.transform.position).normalized;
            mPlayer.mSkillManager.UseChainLightningSkill(direction);
        }
    }
}
