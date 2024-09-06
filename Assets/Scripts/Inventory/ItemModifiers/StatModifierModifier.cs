using UnityEngine;

[CreateAssetMenu(fileName = "_StatModifier", menuName = "WeaponModifiers/StatModifier")]
public class StatModifierModifier : ItemModifierSO, IItemModifier
{
    public StatModifier modifier;
    
    public void ApplyModifier(PlayerController aPlayer)
    {
        modifier = Instantiate(modifier);
        modifier.value *= modifierLevel;
        aPlayer.playerStats.ApplyModifier(modifier);
    }

    public void RemoveModifier(PlayerController aPlayer)
    {
        aPlayer.playerStats.RemoveModifier(modifier);
    }

    public string GetDescription(int aModifierLevel)
    {
        return modifier.GetDescription(aModifierLevel);
    }
}
