using UnityEngine;

[CreateAssetMenu(menuName = "Item/EquipableItemData")]
public class EquipableItemData : ItemData
{
    public EquipmentSocket equipSlot;
    public StatModifier[] statModifiers;
    public GameObject EquipableItemPrefab;
}