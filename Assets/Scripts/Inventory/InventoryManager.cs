using UnityEngine;
using System.Collections.Generic;
using System;

public class InventoryManager : MonoBehaviour
{
    private const int MaxItems = 30; // Límite máximo de ítems
    private const int InventoryWidth = 6; // Ancho de la cuadrícula del inventario
    private const int InventoryHeight = 5; // Altura de la cuadrícula del inventario
    [SerializeField] private int currentItemControllerCount = 0;

    private int currentSelectionIndex = 0;
    private List<ItemController> itemControllerPool = new List<ItemController>();
    private List<ItemData> inventoryItems = new List<ItemData>();

    [SerializeField] private Transform itemContent;
    [SerializeField] private GameObject itemControllerPrefab;
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] public  EquipmentManager equipmentManager;
    [SerializeField] private SelectedItemUI selectedItemUI;



    private void Start()
    {
        equipmentManager = GetComponent<EquipmentManager>();
        InitializeItemControllerPool(MaxItems);
        inventoryItems.Clear();
        currentItemControllerCount = 0;
        ToggleInventory();
    }
 

    private void Update()
    {
        if (!inventoryCanvas.activeInHierarchy) return;
        HandleInput();
    }

    private void InitializeItemControllerPool(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject tmpItem = Instantiate(itemControllerPrefab, itemContent);
            tmpItem.SetActive(false);
            itemControllerPool.Add(tmpItem.GetComponent<ItemController>());
        }
    }

    public void AddItem(ItemData newItem)
    {

        if (newItem == null)
        {
            return;
        }
        newItem = Instantiate(newItem);
        if (newItem.isStackable)
        {
            ItemData existingItem = inventoryItems.Find(item => item.id == newItem.id);
            if (existingItem != null)
            {
                existingItem.quantity += newItem.quantity;
                UpdateItemControllerUI(existingItem);
                return;
            }
        }

        // Si el inventario está lleno, no se puede agregar el ítem
        if (inventoryItems.Count >= MaxItems)
        {
            Debug.Log("Inventario lleno");
            return;
        }

        // Agregar el nuevo ítem al inventario
        if (newItem is EquipableItemData equipableItem)
        {
            equipmentManager.InstatiateItem(equipableItem);
            selectedItemUI.AddNewItemToDictionary(equipableItem);
        }
        inventoryItems.Add(newItem);
        AssignItemToController(newItem);
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
                if(existingItem is EquipableItemData equipable)
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
            DeselectItem();
            inventoryCanvas.SetActive(false);
        }
        else
        {
            ListItemControllers();
            HighlightSelectedItem();    
            inventoryCanvas.SetActive(true); 
        }

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
            controller.gameObject.SetActive(false);
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
           // controller.ResetItemController();
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

        // Mantener la selección dentro de los límites de la cuadrícula
        if (currentSelectionIndex < 0)
        {
            currentSelectionIndex = currentItemControllerCount - 1;
        }
        else if (currentSelectionIndex >= currentItemControllerCount)
        {
            currentSelectionIndex = 0;
        }
        /*
        else if (currentSelectionIndex % InventoryWidth == 0 && direction == -1)
        {
            currentSelectionIndex += InventoryWidth - 1;
        }
        else if ((currentSelectionIndex + 1) % InventoryWidth == 0 && direction == 1)
        {
            currentSelectionIndex -= InventoryWidth - 1;
        }
         */

        HighlightSelectedItem();
    }

    private void HighlightSelectedItem()
    {
        if (currentSelectionIndex < inventoryItems.Count)
        {
            var selectedItem = itemControllerPool[currentSelectionIndex];
            selectedItem.SelectItemController();
            if(selectedItemUI!= null && selectedItem.GetItemData() is EquipableItemData equipable)
            {
                selectedItemUI.DisplayNewItem(equipable);
            }
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
    private void HandleInput()
    {
        if (currentItemControllerCount == 0) return;
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveSelection(-InventoryWidth); // Mueve hacia arriba
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveSelection(InventoryWidth); // Mueve hacia abajo
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveSelection(-1); // Mueve hacia la izquierda
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveSelection(1); // Mueve hacia la derecha
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            EquipSelectedItem(); // Equipar el ítem seleccionado
        }
    }
    private void EquipSelectedItem()
    {
        if (currentSelectionIndex < inventoryItems.Count)
        {
            ItemData selectedItem = inventoryItems[currentSelectionIndex];
            if (selectedItem != null)
            {
                if (selectedItem is EquipableItemData equipableItem)
                { 

                    if (equipmentManager != null)
                    {
                        equipmentManager.EquipItem(equipableItem);
                    }
                    else
                    {
                        Debug.LogError("No se encontró un EquipmentManager en el GameObject.");
                    }
                }
                else
                {
                    Debug.LogWarning("El ítem seleccionado no es equipable.");
                }
            }
        }
    }


 
} 
 