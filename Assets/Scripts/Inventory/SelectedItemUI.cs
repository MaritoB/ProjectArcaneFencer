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
    Dictionary<EquipableItemData, GameObject> ItemModelsDictionary = new Dictionary<EquipableItemData, GameObject>();
    EquipableItemData DisplayedItem;

    private void Awake()
    {
        if (ModifiersContent == null || ItemModifierTextPrefab == null || ItemName == null || ItemModelUIParent == null)
        {
            Debug.LogError("Some references are missing in the Inspector!");
        }
    }

    void Start()
    {
        ModifierTexts = new TMPro.TextMeshProUGUI[MaxModifiers];
        for (int i = 0; i < ModifierTexts.Length; i++)
        {
            GameObject tmpItem = Instantiate(ItemModifierTextPrefab, ModifiersContent);
            ModifierTexts[i] = tmpItem.GetComponent<TMPro.TextMeshProUGUI>();
            tmpItem.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (ItemModelUIParent == null) return;
        ItemModelUIParent.transform.Rotate(Vector3.left, rotationSpeed * Time.fixedDeltaTime);
    }

    /*
    public void RemoveItem(EquipableItemData itemToRemove)
    {
        if (itemToRemove == null) return;

        if (ItemModelsDictionary.TryGetValue(itemToRemove, out GameObject itemModel))
        {
            ItemModelsDictionary.Remove(itemToRemove);
            Destroy(itemModel);
        }

        ClearItem();
    }

    public void AddNewItemToDictionary(EquipableItemData aNewItemEquipable)
    {
        if (aNewItemEquipable == null || aNewItemEquipable.EquipableItemPrefab == null) return;

        GameObject newModel = Instantiate(aNewItemEquipable.EquipableItemPrefab, ItemModelUIParent.transform);
        SetLayerForAllChildren(newModel.transform, 5);

        if (!ItemModelsDictionary.ContainsKey(aNewItemEquipable))
        {
            ItemModelsDictionary.Add(aNewItemEquipable, newModel);
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
     */
    public void AddNewItemToDictionary(EquipableItemData aNewItemEquipable)
    {
        UIUtilities.AddNewItemToDictionary(aNewItemEquipable, ItemModelUIParent.transform, ItemModelsDictionary);
    }
    public void RemoveItem(EquipableItemData itemToRemove)
    {
        UIUtilities.RemoveItemFromDictionary(itemToRemove, ItemModelsDictionary);
        ClearItem();
    }
    public void DisplayNewItem(EquipableItemData aNewItem)
    {
        if (aNewItem == DisplayedItem || aNewItem == null) return;

        ClearItem();
        DisplayedItem = aNewItem;
        ItemName.text = aNewItem.displayName;

        for (int i = 0; i < aNewItem.itemModifiers.Count && i < ModifierTexts.Length; i++)
        {
            if (aNewItem.itemModifiers[i].ItemModifier == null)
            {
                Debug.Log("NULL item Modifiers");
            }
            if (aNewItem.itemModifiers[i].ItemModifier is IItemModifier modifier)
            {
                ModifierTexts[i].text = modifier.GetDescription(aNewItem.itemModifiers[i].level);
            }
            ModifierTexts[i].gameObject.SetActive(true);
        }

        if (ItemModelsDictionary.TryGetValue(aNewItem, out GameObject itemModel))
        {
            itemModel.SetActive(true);
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