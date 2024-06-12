
    using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] ItemData mItem;
    SpriteRenderer sprite;
    [SerializeField] LayerMask playerLayerMask;
    private void Start()
    {
        /*
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = mItem.icon;
      */
    }
    void PickUp(InventoryManager inventory)
    {
        inventory.Add(mItem);
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