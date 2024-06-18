
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI unitPriceText;
    [SerializeField] TextMeshProUGUI itemQuantityText;
    [SerializeField] Image itemIcon;
    [SerializeField] Button button;
    [SerializeField] ItemData itemData = null; 
    public void SetNewItemData(ItemData aItemData) { 
        itemData = Instantiate(aItemData); 
        UpdateItemControllerUI();

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
        if (itemData.ColorMultiplier != Color.clear)
        {
            itemIcon.color = itemData.ColorMultiplier;
        }
    }

    public void ResetItemController()
    {
        itemData = null;
        button.onClick.RemoveAllListeners();
        unitPriceText.text = "#";
        itemQuantityText.text = "#"; 
    } 
    public ItemData GetItemData()
    {
        return itemData;
    }
}