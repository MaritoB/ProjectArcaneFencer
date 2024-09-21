using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
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

    private InventoryManager inventoryManager;

    // Inicializar el InventoryManager
    public void Initialize(InventoryManager manager)
    {
        inventoryManager = manager;
    }

    public void SetNewItemData(ItemData aItemData)
    {
        itemData = aItemData;
        if (itemData is EquipableItemData equipable)
        {
            itemIcon.gameObject.SetActive(false);
            itemQuantityText.gameObject.SetActive(false);
            if (UIitem3D == null)
            {
                UIitem3D = Instantiate(equipable.EquipableItemPrefab, itemParent);
                SetLayerForAllChildren(UIitem3D.transform, 5);
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
    }

    public ItemData GetItemData()
    {
        return itemData;
    }

    // Detectar cuando el mouse entra sobre el ítem
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse sobre el item.");  // Para verificar si funciona
        if (inventoryManager != null)
        {
            inventoryManager.HighlightSelectedItemWithMouse(this);
        }
    }

    // Detectar clic (clic derecho en este caso)
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (inventoryManager != null)
            {
                inventoryManager.EquipItemWithMouse(this);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inventoryManager != null)
        {
            inventoryManager.DeselectItem(this);
        }
    }
}
