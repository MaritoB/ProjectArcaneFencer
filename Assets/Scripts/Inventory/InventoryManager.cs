
    using TMPro;
    using UnityEngine;
    using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    List<ItemController> ItemControllerPool;
    List<ItemData> InventoryItemsList;
    [SerializeField] Transform ItemContent;
    // [SerializeField] PlayerOutfitManager outfitManager;
    [SerializeField] GameObject ItemControllerPrefab;
    [SerializeField] GameObject InventoryCanvas;
    [SerializeField] GameObject InventoryCamera;
    [SerializeField] TextMeshProUGUI GoldAmountText;
    [SerializeField] int goldAmount;
    InventoryManager customerInventory;

    private void Start()
    {
        ItemControllerPool = new List<ItemController>();
        InventoryItemsList = new List<ItemData>();

        GameObject tmpItem;
        for (int i = 0; i < 3; i++)
        {
            tmpItem = Instantiate(ItemControllerPrefab, ItemContent);
            tmpItem.SetActive(false);
            ItemControllerPool.Add(tmpItem.GetComponent<ItemController>());
        }
    }

    public ItemController AddItemControllerToPool()
    {
        GameObject tmpItem;
        tmpItem = Instantiate(ItemControllerPrefab, ItemContent);
        ItemController tempItemControllertmpItem = tmpItem.GetComponent<ItemController>();
        tempItemControllertmpItem.ResetItemController();
        ItemControllerPool.Add(tempItemControllertmpItem);
        return tempItemControllertmpItem;
    }
    public ItemController GetPooledItemController()
    {
        for (int i = 0; i < ItemControllerPool.Count; i++)
        {
            if (!ItemControllerPool[i].gameObject.activeSelf)
            {
                return ItemControllerPool[i];
            }
        }
        return AddItemControllerToPool(); ;
    }
    public void SellItem(ItemData aItem)
    {
        if (aItem == null)
        {
            return;
        }
        goldAmount += aItem.goldPrice;
        UpdateGold();
        Remove(aItem);
        customerInventory.Add(aItem);
        ListItemControllers();
        SetOnClickSell();
        customerInventory.ListItemControllers();
        customerInventory.SetOnClickBuy();
    }
    public void TryToBuy(ItemData aItem)
    {
        if (aItem == null)
        {
            return;
        }
        if (aItem.goldPrice <= customerInventory.goldAmount)
        {
            customerInventory.goldAmount -= aItem.goldPrice;
            customerInventory.UpdateGold();
            Remove(aItem);
            customerInventory.Add(aItem);
            ListItemControllers();
            SetOnClickBuy();
            customerInventory.ListItemControllers();
            customerInventory.SetOnClickSell();
        }
        else
        {
            Debug.Log("Need More Gold to Buy");
        }
    }
    public void Add(ItemData aItem)
    {
        if (InventoryItemsList == null || aItem == null)
        {
            return;
        }
        InventoryItemsList.Add(aItem);
        AddItemToItemController(aItem);
    }
    private void AddItemToItemController(ItemData aItem)
    {
        ItemController itemController = ItemControllerPool.Find(x => x.GetItemData() == aItem);
        if (itemController != null)
        {
            itemController.addToStack();
            itemController.gameObject.SetActive(true);
            itemController.UpdateItemControllerUI();
        }
        else
        {
            itemController = GetPooledItemController();
            itemController.gameObject.SetActive(true);
            itemController.SetNewItemData(aItem, 1);
        }
    }
    public void Remove(ItemData aItem)
    {
        if (InventoryItemsList == null || aItem == null)
        {
            return;
        }
        InventoryItemsList.Remove(aItem);
        ItemController itemController = ItemControllerPool.Find(x => x.GetItemData() == aItem);
        if (itemController != null)
        {
            itemController.RemoveFromStack();
            if (itemController.GetStackSize() <= 0)
            {
                itemController.ResetItemController();
                itemController.gameObject.SetActive(false);
            }
        }
    }
    public void ListItemControllers()
    {
        foreach (Transform itemCont in ItemContent)
        {
            itemCont.GetComponent<ItemController>().ResetItemController();
            itemCont.gameObject.SetActive(false);
        }
        foreach (var item in InventoryItemsList)
        {
            AddItemToItemController(item);
        }
    }
    public void SetOnClickEquip()
    {

        foreach (var itemController in ItemControllerPool)
        {
            ItemData itemData = itemController.GetItemData();
            if (itemController.gameObject.activeSelf && itemData != null)
            {
                itemController.GetButton().onClick.RemoveAllListeners();
                //itemController.GetButton().onClick.AddListener(delegate { outfitManager.EquipItem(itemData); });
            }

        }
    }
    public void SetOnClickBuy()
    {

        foreach (var itemController in ItemControllerPool)
        {
            ItemData itemData = itemController.GetItemData();
            if (itemController.gameObject.activeSelf && itemData != null)
            {
                itemController.GetButton().onClick.RemoveAllListeners();
                itemController.GetButton().onClick.AddListener(delegate { TryToBuy(itemData); });
            }
        }
    }
    public void SetOnClickSell()
    {

        foreach (var itemController in ItemControllerPool)
        {
            ItemData itemData = itemController.GetItemData();
            if (itemController.gameObject.activeSelf && itemData != null)
            {
                itemController.GetButton().onClick.RemoveAllListeners();
                itemController.GetButton().onClick.AddListener(delegate { SellItem(itemData); });
            }
        }
    }

    public void SetCustomer(InventoryManager aCustomer)
    {
        customerInventory = aCustomer;
    }
    public void EquipItem(ItemData aItem)
    {
        // outfitManager.EquipItem(aItem);
    }
    public void UpdateGold()
    {
        GoldAmountText.text = "$ " + goldAmount.ToString();
    }
    public void ToggleInventoryOnToBuy()
    {
        ListItemControllers();
        SetOnClickBuy();
        InventoryCanvas.SetActive(true);
        InventoryCamera.SetActive(true);
    }
    public void ToggleInventoryOnToEquip()
    {
        UpdateGold();
        ListItemControllers();
        SetOnClickEquip();
        InventoryCanvas.SetActive(true);
        InventoryCamera.SetActive(true);
    }
    public void ToggleInventoryOnToSell()
    {
        UpdateGold();
        ListItemControllers();
        SetOnClickSell();
        InventoryCanvas.SetActive(true);
        InventoryCamera.SetActive(true);
    }
    public void ToggleInventoryOff()
    {
        InventoryCanvas.SetActive(false);
        InventoryCamera.SetActive(false);
    }
}