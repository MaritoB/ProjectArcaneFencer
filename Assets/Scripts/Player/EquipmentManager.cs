using UnityEngine;
using System.Collections.Generic;

public class EquipmentManager : MonoBehaviour
{
    private Dictionary<EquipmentSocket, EquipableItemData> equippedItems = new Dictionary<EquipmentSocket, EquipableItemData>();
    [SerializeField] public Dictionary<EquipmentSocket, Transform> itemSockets = new Dictionary<EquipmentSocket, Transform>();

    [SerializeField]private CharacterStats playerStats;

    [SerializeField]
    private List<ItemSocketMapping> itemSocketMappings;

    private void Start()
    {
        playerStats = GetComponentInParent<CharacterStats>();
        foreach (var mapping in itemSocketMappings)
        {
            itemSockets[mapping.itemType] = mapping.socket;
        }
    }
    public void InstatiateItem(EquipableItemData aEquipableItem)
    {
        if (aEquipableItem.EquipableItemPrefab != null && itemSockets.TryGetValue(aEquipableItem.equipSlot, out Transform socket))
        {
            aEquipableItem.EquipableItemPrefab = Instantiate(aEquipableItem.EquipableItemPrefab, socket);
            aEquipableItem.EquipableItemPrefab.SetActive(false);
        }
    }

    public void EquipItem(EquipableItemData item)
    {
        if (item == null)
        {
            Debug.LogWarning("El ítem es null y no puede ser equipado.");
            return;
        }

        if (equippedItems.TryGetValue(item.equipSlot, out EquipableItemData currentlyEquippedItem))
        {
            UnequipItem(currentlyEquippedItem);
        }

        equippedItems[item.equipSlot] = item;

        foreach (var modifier in item.statModifiers)
        {
            playerStats.ApplyModifier(modifier);
        }

        if (item.EquipableItemPrefab != null && itemSockets.TryGetValue(item.equipSlot, out Transform socket))
        {
            item.EquipableItemPrefab.SetActive(true); 
        }

        Debug.Log(item.name + " equipado en el slot " + item.equipSlot);
    }

    public void UnequipItem(EquipableItemData item)
    {
        if (item != null && equippedItems.ContainsKey(item.equipSlot))
        {
            foreach (var modifier in item.statModifiers)
            {
                playerStats.RemoveModifier(modifier);
            }

            item.EquipableItemPrefab.SetActive(false);
            equippedItems.Remove(item.equipSlot); 

            Debug.Log(item.name + " fue desequipado del slot " + item.equipSlot);
        }
    }

    public EquipableItemData GetEquippedItem(EquipmentSocket slot)
    {
        equippedItems.TryGetValue(slot, out EquipableItemData equippedItem);
        return equippedItem;
    }
}
[System.Serializable]
public struct ItemSocketMapping
{
    public EquipmentSocket itemType;
    public Transform socket;
}


public enum EquipmentSocket
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