using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItemUI : MonoBehaviour
{ 
    private const int MaxModifiers = 6;
    [SerializeField] float rotationSpeed = 50;

    [SerializeField] private Transform ModifiersContent;
    [SerializeField] private GameObject ItemModifierTextPrefab;
    public GameObject ItemModelUIParent, ItemModelUI; 
    [SerializeField] TMPro.TextMeshProUGUI ItemName;
    TMPro.TextMeshProUGUI[] ModifierTexts;
    Dictionary<EquipableItemData, GameObject> ItemModelsDictionary;
    EquipableItemData DisplayedItem;
    void Start()
    {
        ModifierTexts = new TMPro.TextMeshProUGUI[MaxModifiers];
        for (int i = 0; i < ModifierTexts.Length; i++)
        {
            GameObject tmpItem = Instantiate(ItemModifierTextPrefab, ModifiersContent);
            ModifierTexts[i] = tmpItem.GetComponent<TMPro.TextMeshProUGUI>();
            tmpItem.SetActive(false);
        }
        ItemModelsDictionary = new Dictionary<EquipableItemData, GameObject>();

    }


    private void FixedUpdate()
    { 
        ItemModelUIParent.transform.Rotate(Vector3.left, rotationSpeed  * Time.fixedDeltaTime);
    }
    public void RemoveItem(EquipableItemData itemToRemove)
    {
        if (itemToRemove == null)
        {
            Debug.LogWarning("The item to remove is null.");
            return;
        }

        if (ItemModelsDictionary.TryGetValue(itemToRemove, out GameObject itemModel))
        {
            ItemModelsDictionary.Remove(itemToRemove);
            Destroy(itemModel);
            Debug.Log(itemToRemove.name + " was removed and its model destroyed.");
        }
        else
        {
            Debug.LogWarning("The item was not found in the dictionary.");
        }

        ClearItem();
    }
    public void AddNewItemToDictionary(EquipableItemData aNewItemEquipable)
    {
        if (aNewItemEquipable == null || aNewItemEquipable.EquipableItemPrefab == null)
        {
            Debug.LogWarning("Item null. cant be added to dictionary.");
            return;
        } 
        GameObject newModel = Instantiate(aNewItemEquipable.EquipableItemPrefab, ItemModelUIParent.transform);
         
        SetLayerForAllChildren(newModel.transform, 5);
         
        if (!ItemModelsDictionary.ContainsKey(aNewItemEquipable))
        {
            ItemModelsDictionary.Add(aNewItemEquipable, newModel);
        }
        else
        {
            Debug.LogWarning("El ítem ya existe en el diccionario. No se agregó nuevamente.");
        }
    }
     
    private void SetLayerForAllChildren(Transform parentTransform, int layer)
    {
        parentTransform.gameObject.layer = layer;
        foreach (Transform child in parentTransform)
        {
            child.gameObject.layer = layer; 
            SetLayerForAllChildren(child, layer);
        }
    }

    public void DisplayNewItem(EquipableItemData aNewItem)
    { 
        if(aNewItem  == DisplayedItem || aNewItem == null)
        {
            return;
        }
        ClearItem();
        DisplayedItem = aNewItem;
        ItemName.text = aNewItem.displayName;

        for (int i = 0; i < aNewItem.statModifiers.Length; i++)
        {
            ModifierTexts[i].text = aNewItem.statModifiers[i].description;
            ModifierTexts[i].gameObject.SetActive(true);
        }

        if (ItemModelsDictionary.TryGetValue(aNewItem, out GameObject itemModel))
        {
            itemModel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("The item model was not found in the dictionary.");
        }
    }
    private void ClearItem()
    {
        foreach (var model in ItemModelsDictionary.Values)
        {
            model.SetActive(false);
        }

        ItemName.text = "";

        foreach (var text in ModifierTexts)
        {
            text.text = "";
            text.gameObject.SetActive(false);
        }
    }

}
