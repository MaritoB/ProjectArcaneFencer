using UnityEngine;
using UnityEngine.UI;

public class WeaponModifierSO : ScriptableObject
{
    public string modifierName;
    public string modifierDescription;
    public int modifierLevel;
    public Sprite modifierSprite;
    public virtual void ApplyModifier(PlayerController aPlayer)
    {

    }
    // Aquí podrías agregar más atributos según tus necesidades
}
