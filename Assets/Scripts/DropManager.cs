using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{ 
     
    public static DropManager Instance;  
    public Transform ItemDropPrefab;
    ItemPickUp ItemDrop;
    [SerializeField] ItemData GoldItemData;
    public List<ItemData> AllItemDataList;
    WeaponFactory weaponFactory;
    [SerializeField] int dropChance;

    private List<ItemPickUp> ItemDrops = new List<ItemPickUp>();
    private void Start()
    {
        ItemDrop = ItemDropPrefab.GetComponent<ItemPickUp>();
        weaponFactory = GetComponent<WeaponFactory>();
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (ItemDrop == null)
            {
                return;
            }
            PreInstantiate(ItemDrops, ItemDrop, 10);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private ItemPickUp GetFromPool(List<ItemPickUp> pool, ItemPickUp prefab)
    {
        foreach (var item in pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                item.ClearItemsData();
                return item;
            }
        }

        var newInstance = Instantiate(prefab);
        pool.Add(newInstance);
        newInstance.transform.SetParent(transform);

        return newInstance;
    }
    private void PreInstantiate(List<ItemPickUp> pool, ItemPickUp prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var instance = Instantiate(prefab); 
            instance.gameObject.SetActive(false);
            pool.Add(instance);
            instance.transform.SetParent(transform);
        }
    }
    public ItemPickUp GetDropItem()
    {
        return GetFromPool(ItemDrops, ItemDrop);
    }
    public void DropRandomItem(int aDropLevel, Vector3 aPosition)
    {
        ItemPickUp newItemToDrop = GetDropItem();
        newItemToDrop.transform.position = aPosition;
        ItemData newGold = Instantiate(GoldItemData);
        newGold.quantity = aDropLevel;
        if(newGold.quantity > 0)
        {
            newItemToDrop.AddItemData(newGold);
            newItemToDrop.gameObject.SetActive(true);
        }
        if (Random.Range(0, 100) < dropChance)
        {
            newItemToDrop.AddItemData(weaponFactory.CreateWeapon()); 
        }
        int randomNumberDrop = Random.Range(0, AllItemDataList.Count);
        ItemData newDropItem = Instantiate(AllItemDataList[randomNumberDrop]);
        
        newItemToDrop.AddItemData(newDropItem);


    }

}

