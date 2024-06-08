
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI unitPriceText;
    [SerializeField] TextMeshProUGUI itemQuantityText;
    [SerializeField] Image itemIcon;
    [SerializeField] Button button;

    ItemData itemData = null;
    int stackSize = 0;
    public void SetNewItemData(ItemData aItemData, int aStackSize)
    {
        itemData = aItemData;
        stackSize = aStackSize;
        UpdateItemControllerUI();

    }
    public Button GetButton()
    {
        return button;
    }
    public void UpdateItemControllerUI()
    {
        unitPriceText.text = itemData.goldPrice.ToString();
        itemQuantityText.text = stackSize.ToString();
        itemIcon.sprite = itemData.icon;
    }

    public void ResetItemController()
    {
        itemData = null;
        button.onClick.RemoveAllListeners();
        unitPriceText.text = "#";
        itemQuantityText.text = "#";
        stackSize = 0;
    }

    public int GetStackSize()
    {
        return stackSize;
    }
    public void addToStack()
    {
        stackSize++;
    }
    public void RemoveFromStack()
    {
        stackSize--;
    }
    public ItemData GetItemData()
    {
        return itemData;
    }
}