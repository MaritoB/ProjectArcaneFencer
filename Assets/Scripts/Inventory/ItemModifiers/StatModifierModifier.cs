using UnityEngine;

[CreateAssetMenu(fileName = "_StatModifier", menuName = "WeaponModifiers/StatModifier")]
public class StatModifierModifier : ItemModifierSO, IItemModifier
{
    public StatModifier modifier;
    
    public void ApplyModifier(PlayerController aPlayer)
    {
        aPlayer.playerStats.ApplyModifier(modifier);
    }

    public void RemoveModifier(PlayerController aPlayer)
    {
        aPlayer.playerStats.RemoveModifier(modifier);
    }

    public string GetDescription()
    {
        return modifier.GetDescription();
    }
}
