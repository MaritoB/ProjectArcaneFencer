using System.Collections.Generic;
using UnityEngine;

public static class UIUtilities
{ 
    public static void AddNewItemToDictionary(EquipableItemData aNewItemEquipable, Transform parentTransform, Dictionary<EquipableItemData, GameObject> aDirectionary)
    {
        if (aNewItemEquipable == null || aNewItemEquipable.EquipableItemPrefab == null) return;

        if (!aDirectionary.ContainsKey(aNewItemEquipable))
        {
            GameObject newModel = GameObject.Instantiate(aNewItemEquipable.EquipableItemPrefab, parentTransform);
            SetLayerForAllChildren(newModel.transform, 5);
            aDirectionary.Add(aNewItemEquipable, newModel);
        }
    } 
    public static void RemoveItemFromDictionary(EquipableItemData itemToRemove, Dictionary<EquipableItemData, GameObject> aDirectionary)
    {
        if (itemToRemove == null) return;

        if (aDirectionary.TryGetValue(itemToRemove, out GameObject itemModel))
        {
            aDirectionary.Remove(itemToRemove);
            GameObject.Destroy(itemModel);
        }
    }
     
    public static void SetLayerForAllChildren(Transform parentTransform, int layer)
    {
        parentTransform.gameObject.layer = layer;
        foreach (Transform child in parentTransform)
        {
            child.gameObject.layer = layer;
            SetLayerForAllChildren(child, layer);
        }
    }
}
