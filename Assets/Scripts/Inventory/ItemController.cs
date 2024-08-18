
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI unitPriceText;
    [SerializeField] TextMeshProUGUI itemQuantityText;
    [SerializeField] Image itemIcon;
    [SerializeField] Image BackgroundImage;
    [SerializeField] Button button;
    [SerializeField] ItemData itemData = null;
    [SerializeField] Color NormalColor;
    [SerializeField] Color HighlitedColor;
    [SerializeField] RectTransform itemParent;
    public GameObject UIitem3D = null;


    public void SetNewItemData(ItemData aItemData) { 
        itemData = aItemData;
        if (itemData is EquipableItemData equipable)
        {
            itemIcon.gameObject.SetActive(false);
            itemQuantityText.gameObject.SetActive(false);
            if(UIitem3D == null)
            {
                UIitem3D = Instantiate(equipable.EquipableItemPrefab, itemParent);
                SetLayerForAllChildren(UIitem3D.transform,5);
            }
            UIitem3D.SetActive(true);
        }
        else
        {
            itemIcon.gameObject.SetActive(true);
            itemQuantityText.gameObject.SetActive(true);

        }
        UpdateItemControllerUI(); 
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
    public void SelectItemController()
    {
        BackgroundImage.color = HighlitedColor;
    }
    public void DeselectItemController()
    {
        BackgroundImage.color = NormalColor;
    }
    public Button GetButton()
    {
        return button;
    }
    public void UpdateItemControllerUI()
    {
        if (itemData.quantity == 0)
        {
            ResetItemController();
            gameObject.SetActive(false);
            return;
        }
        unitPriceText.text = itemData.goldPrice.ToString();
        itemQuantityText.text = itemData.quantity.ToString();
        itemIcon.sprite = itemData.icon;
        if (itemData.colorMultiplier != Color.clear)
        {
            itemIcon.color = itemData.colorMultiplier;
        }
    }

    public void ResetItemController()
    {
        Destroy(UIitem3D);
        itemData = null;
        /*
        button.onClick.RemoveAllListeners();
        unitPriceText.text = "#";
        itemQuantityText.text = "#"; 
         */
    } 
    public ItemData GetItemData()
    {
        return itemData;
    }
}