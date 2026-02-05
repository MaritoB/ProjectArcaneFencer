using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    private const int MaxItems = 15;
    private const int InventoryWidth = 6;
    private const int InventoryHeight = 5;
    [SerializeField] private int currentItemControllerCount = 0;

    private int currentSelectionIndex = 0;
    private List<ItemController> itemControllerPool = new List<ItemController>();
    private List<ItemData> inventoryItems = new List<ItemData>();

    [SerializeField] private Transform itemContent;
    [SerializeField] private GameObject itemControllerPrefab;
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] public EquipmentManager equipmentManager;
    [SerializeField] private SelectedItemUI selectedItemUI;

    private void Start()
    {
        equipmentManager = GetComponent<EquipmentManager>();
        if (equipmentManager == null)
        {
            Debug.LogError("EquipmentManager no est� asignado correctamente.");
        }
        InitializeItemControllerPool(MaxItems);
        inventoryItems.Clear();
        currentItemControllerCount = 0;
        //ToggleInventory();
    }
    private void InitializeItemControllerPool(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject tmpItem = Instantiate(itemControllerPrefab, itemContent);
            //tmpItem.SetActive(false);
            ItemController Controller = tmpItem.GetComponent<ItemController>();
            Controller.Initialize(this);
            itemControllerPool.Add(Controller);
        }
    }

    public bool AddItem(ItemData newItem)
    {
        if (newItem == null) return false;
        newItem = Instantiate(newItem);

        if (newItem.isStackable)
        {
            ItemData existingItem = inventoryItems.Find(item => item.id == newItem.id);
            if (existingItem != null)
            {
                existingItem.quantity += newItem.quantity;
                UpdateItemControllerUI(existingItem);
                return true;
            }
        }

        if (inventoryItems.Count >= MaxItems)
        {
            Debug.Log("Inventario lleno");
            return false;
        }

        if (newItem is EquipableItemData equipableItem)
        {
            equipmentManager.InstatiateItem(equipableItem);
            selectedItemUI.AddNewItemToDictionary(equipableItem);
        }

        inventoryItems.Add(newItem);
        AssignItemToController(newItem);

        Debug.Log("item quantity:"+ inventoryItems.Count);
        return true;
    }

    public void RemoveItem(ItemData itemToRemove, int quantityToRemove)
    {
        ItemData existingItem = inventoryItems.Find(item => item.id == itemToRemove.id);
        if (existingItem != null && existingItem.quantity >= quantityToRemove)
        {
            existingItem.quantity -= quantityToRemove;
            if (existingItem.quantity <= 0)
            {
                inventoryItems.Remove(existingItem);
                if (existingItem is EquipableItemData equipable)
                {
                    selectedItemUI.RemoveItem(equipable);
                }
                DeactivateItemController(existingItem);
            }
            else
            {
                UpdateItemControllerUI(existingItem);
            }
        }
    }

    public void ToggleInventory()
    {
        if (inventoryCanvas.activeInHierarchy)
        {
            CloseInventory();
        }
        else
        {
            ListItemControllers();
            HighlightSelectedItem(); 
            UIManager.Instance.OpenInvetory();
        }
    }
    public void CloseInventory()
    { 
        DeselectItem();
        UIManager.Instance.CloseInventory();
    }

    private void AssignItemToController(ItemData itemData)
    {
        ItemController controller = itemControllerPool.Find(c => !c.gameObject.activeSelf);
        if (controller != null)
        {
            controller.gameObject.SetActive(true);
            controller.SetNewItemData(itemData);
            currentItemControllerCount++;
        }
    }

    private void DeactivateItemController(ItemData itemData)
    {
        ItemController controller = itemControllerPool.Find(c => c.GetItemData() == itemData);
        if (controller != null)
        {
            controller.ResetItemController();
            //controller.gameObject.SetActive(false);
        }
    }

    private void UpdateItemControllerUI(ItemData itemData)
    {
        ItemController controller = itemControllerPool.Find(c => c.GetItemData() == itemData);
        if (controller != null)
        {
            controller.UpdateItemControllerUI();
        }
    }

    public void ListItemControllers()
    {
        foreach (ItemController controller in itemControllerPool)
        {
            controller.gameObject.SetActive(false);
            currentItemControllerCount = 0;
        }

        foreach (ItemData item in inventoryItems)
        {
            AssignItemToController(item);
        }
    }

    private void MoveSelection(int direction)
    {
        DeselectItem();

        currentSelectionIndex += direction;

        if (currentSelectionIndex < 0)
        {
            currentSelectionIndex = currentItemControllerCount - 1;
        }
        else if (currentSelectionIndex >= currentItemControllerCount)
        {
            currentSelectionIndex = 0;
        }

        HighlightSelectedItem();
    }

    public void HighlightSelectedItem()
    {
        if (currentSelectionIndex < inventoryItems.Count)
        {
            var selectedItem = itemControllerPool[currentSelectionIndex];
            selectedItem.SelectItemController();
            if (selectedItemUI != null && selectedItem.GetItemData() is EquipableItemData equipable)
            {
                selectedItemUI.DisplayNewItem(equipable);
            }
        }
    }
    public void HighlightSelectedItemWithMouse(ItemController selectedItem)
    { 
        selectedItem.SelectItemController();
        if (selectedItemUI != null && selectedItem.GetItemData() is EquipableItemData equipable)
        {
            selectedItemUI.DisplayNewItem(equipable);
        }
    }
    public void EquipItemWithMouse(ItemController selectedItem)
    {
        ItemData itemData = selectedItem.GetItemData();
        if (itemData is EquipableItemData equipableItem)
        {
            equipmentManager?.EquipItem(equipableItem);
        }
    }

    private void DeselectItem()
    {
        if (currentSelectionIndex < inventoryItems.Count)
        {
            var selectedItem = itemControllerPool[currentSelectionIndex];
            selectedItem.DeselectItemController();
        }
    }
    public void DeselectItem(ItemController selectedItem)
    { 
        selectedItem = itemControllerPool[currentSelectionIndex];
        selectedItem.DeselectItemController();
  
    } 
    private void EquipSelectedItem()
    {
        if (currentSelectionIndex < inventoryItems.Count)
        {
            ItemData selectedItem = inventoryItems[currentSelectionIndex];
            if (selectedItem is EquipableItemData equipableItem)
            {
                equipmentManager?.EquipItem(equipableItem);
            }
        }
    }
}
