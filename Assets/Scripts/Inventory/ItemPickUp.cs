
namespace BlueGravityTest
{

    using UnityEngine;

    public class ItemPickUp : MonoBehaviour
    {
        [SerializeField] ItemData mItem;
        SpriteRenderer sprite;
        private void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            sprite.sprite = mItem.icon;
        }
        void PickUp(InventoryManager inventory)
        {
            inventory.Add(mItem);
            gameObject.SetActive(false);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            InventoryManager inventory = collision.GetComponentInChildren<InventoryManager>();
            if (inventory != null) 
            {
                if (mItem != null)
                {
                    PickUp(inventory);
                }
            }
        }
    }
}
