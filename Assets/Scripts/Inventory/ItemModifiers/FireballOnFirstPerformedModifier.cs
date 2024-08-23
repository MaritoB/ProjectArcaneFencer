using UnityEngine;
[CreateAssetMenu(fileName = "_FireballOnFirstPerformedModifier", menuName = "WeaponModifiers/FireballOnFirstPerformedModifier")]
public class FireballOnFirstPerformedModifier : ItemModifierSO, IItemModifier
{
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int currentTriggerChance;
    PlayerController player;

    public void ApplyModifier(PlayerController aPlayer)
    {
        RemoveModifier(player);
        player = aPlayer;
        player.mSkillManager.SetLevelFireball(modifierLevel);
        aPlayer.inventory.equipmentManager.weapon.OnFirstMeleePerformed += TryCastFireBall;
        currentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
 
    }
    public string GetDescription()
    { 
        return  (TriggerChanceBase + TriggerChanceMultiplier * (modifierLevel + 1)) + "% chance to cast it on First Attack. ";
    }
    public void TryCastFireBall()
    {
        if (player == null) return;
        int number = Random.Range(0, 100);
        if (number < currentTriggerChance)
        {
            player.mSkillManager.UseFireball(player.transform.forward.normalized); 
        } 
    }

    public void RemoveModifier(PlayerController aPlayer)
    {
        if (player != null && aPlayer == player)
        {
            aPlayer.inventory.equipmentManager.weapon.OnFirstMeleePerformed -= TryCastFireBall;
            player = null;
        }
    }
}
