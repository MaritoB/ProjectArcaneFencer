using UnityEngine;

[CreateAssetMenu(fileName = "_ChanceToUseASkillOnPerformedModifier", menuName = "WeaponModifiers/ChanceToUseASkillOnPerformedModifier")]
public class ChanceToUseASkillOnPerformedModifier : ItemModifierSO, IItemModifier
{
    public PerformedEventTypes eventType; // Cambiado a EventMeleeHitType para alinearlo con WeaponBase
    public SkillEnum skill;
    public int TriggerChanceBase, TriggerChanceMultiplier;
    private int currentTriggerChance;
    private PlayerController player;
    private WeaponBase weapon;

    public void ApplyModifier(PlayerController aPlayer)
    {
        RemoveModifier(player); // Remover cualquier modificador anterior
        player = aPlayer;
        weapon = player.inventory?.equipmentManager?.weapon;

        if (weapon == null)
        {
            Debug.LogError("Weapon is null in ApplyModifier.");
            return;
        }

        currentTriggerChance = TriggerChanceBase + (TriggerChanceMultiplier * modifierLevel);

        // Suscribirse al evento basado en el tipo especificado
       // weapon.SubscribeToEvent(eventType, null, TryToUseSkill);

        weapon.SubscribeToEvent(eventType, TryToUseSkill);

    }

    public string GetDescription(int aModifierLevel)
    {
        return $"{TriggerChanceBase + TriggerChanceMultiplier * aModifierLevel}% chance to cast {skill} {eventType}.";
    }

    public void RemoveModifier(PlayerController aPlayer)
    {
        if (player != null && aPlayer == player)
        {
            // Desuscribirse del evento cuando se remueve el modificador
            weapon.UnsubscribeFromEvent(eventType, TryToUseSkill);
            player = null;
        }
    }

    public void TryToUseSkill( )
    { 
        int number = Random.Range(0, 100);
        if (number < currentTriggerChance)
        {  
            player.mSkillManager.UseSkill(skill, player.transform.position);
        }
    }

}
