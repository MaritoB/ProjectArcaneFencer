
    using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] System.Collections.Generic.List<ItemData> mItems;
    SpriteRenderer sprite;
    [SerializeField] LayerMask playerLayerMask;
    public void AddItemData(ItemData aItemData)
    {
        mItems.Add(aItemData);
    }
    public void ClearItemsData()
    {
        mItems.Clear();
    }
    private void Start()
    {
        /*
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = mItem.icon;
      */
    }
    void PickUp(InventoryManager inventory)
    {
        foreach (var item in mItems)
        {
            inventory.Add(item);
            if(item.id == "Gold")
            {
                PopupTextPool.Instance.ShowPopUpGoldPickUp(transform.position +Vector3.down, item.quantity.ToString());

            }
        }
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (playerLayerMask == (playerLayerMask | (1 << other.gameObject.layer)))
        {
            PickUp(other.GetComponent<PlayerController>().inventory);

        }
    }

}