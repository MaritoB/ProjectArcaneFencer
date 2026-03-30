using UnityEngine;
public interface IItemModifier
{
    void ApplyModifier(PlayerController aPlayer);
    void RemoveModifier(PlayerController aPlayer);
    string GetDescription(int aModifierLevel);
}
public enum ModifierType
{
    Fire,
    Cold,
    Lightning, 
    Physic,
    Magic,
    Resistance,
    Stat, 
    Skill
}

public abstract class ItemModifierSO : ScriptableObject  
{
    public string modifierName;
    public string modifierDescription;
    public int modifierLevel;
    public ModifierType[] ModifierTypes;
    //public Sprite modifierSprite;
    
    public bool isThisType(ModifierType aType)
    {
        foreach( ModifierType x in ModifierTypes)
        {
            if (x == aType) return true;
        }
        return false;

    }
}
