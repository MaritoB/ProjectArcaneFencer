using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItemUI : MonoBehaviour
{

    private const int MaxModifiers = 6;  
    [SerializeField] private Transform ModifiersContent;
    [SerializeField] private GameObject ItemModifierTextPrefab;
    public GameObject ItemModelUIParent, ItemModelUI;
    [SerializeField] TMPro.TextMeshProUGUI ItemName;
    TMPro.TextMeshProUGUI[] ModifierTexts; 
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

    public void DisplayNewItem(EquipableItemData aNewItem)
    {
        ClearItem();
        ItemName.text = aNewItem.displayName;
        ItemModelUI = Instantiate(aNewItem.EquipableItemPrefab, ItemModelUIParent.transform);
        ItemModelUI.SetActive(true);
        { 
            foreach (Transform child in ItemModelUIParent.transform)
            {
                child.gameObject.layer = 5;
            }
            foreach (Transform child in ItemModelUI.transform)
            {
                child.gameObject.layer = 5;
            }
        }
        for (int i = 0; i < aNewItem.statModifiers.Length; i++)
        {
            ModifierTexts[i].text = aNewItem.statModifiers[i].description;
            ModifierTexts[i].gameObject.SetActive(true);
        }
    }
    void ClearItem()
    {
        if(ItemModelUI != null)
        {
            Destroy(ItemModelUI);
        }
        for (int i = 0; i < ModifierTexts.Length; i++)
        {

            ModifierTexts[i].text = "";
            ModifierTexts[i].gameObject.SetActive(false);
        }
    }

}
