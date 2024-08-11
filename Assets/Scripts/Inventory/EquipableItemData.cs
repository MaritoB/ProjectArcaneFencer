using UnityEngine;

[CreateAssetMenu(menuName = "Item/EquipableItemData")]
public class EquipableItemData : ItemData
{
    public EquipSlot equipSlot;
    public StatModifier[] statModifiers;
}
public enum EquipSlot
{
    Head,
    Body,
    Legs,
    Feet,
    Hands,
    Weapon,
    Shield,
    Accessory
}