using UnityEngine;
public interface IItemModifier
{
    void ApplyModifier(PlayerController aPlayer);
    void RemoveModifier(PlayerController aPlayer);
    string GetDescription();
}

public abstract class ItemModifierSO : ScriptableObject  
{
    public string modifierName;
    public string modifierDescription;
    public int modifierLevel;
    public Sprite modifierSprite;
     
}
