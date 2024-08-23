using UnityEngine;

[CreateAssetMenu(fileName = "_ChainLightningOnBlockModifier", menuName = "WeaponModifiers/ChainLightningOnBlockModifier")]
public class ChainLightningOnBlockModifier : ItemModifierSO, IItemModifier
{
    PlayerController mPlayer; 
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int CurrentTriggerChance;
    public void ApplyModifier(PlayerController aPlayer)
    {
        mPlayer = aPlayer;
        CurrentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
        aPlayer.OnBlockPerformed -= CastProjectile;
        aPlayer.OnBlockPerformed += CastProjectile;
       // mPlayer.mSkillManager.LevelUpChainLightning(modifierLevel);
        GetDescription();
    }
    public string GetDescription()
    {
       return "+1 to Chain Lightning & "+(TriggerChanceBase + TriggerChanceMultiplier * ( modifierLevel +1 )) + "% chance to cast it when blocking melee damage.";
    }
    public void RemoveModifier(PlayerController aPlayer)
    {
        aPlayer.OnBlockPerformed -= CastProjectile;
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
