using UnityEngine;

public class WeaponModifierSO : ScriptableObject
{
    public string modifierName;
    public string modifierDescription;
    public int modifierLevel;
    public Sprite modifierSprite;
    public virtual void ApplyModifier(PlayerController aPlayer)
    {

    }
    public virtual void UpdateDescription()
    {

    }
}
