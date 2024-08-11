
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
    
    public void SetNewItemData(ItemData aItemData) { 
        itemData = aItemData;
        UpdateItemControllerUI(); 
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