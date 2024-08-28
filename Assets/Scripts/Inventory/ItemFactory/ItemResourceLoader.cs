using System.Collections.Generic;
using UnityEngine;

public class ItemResourceLoader : MonoBehaviour 
{

    [SerializeField] private List<ItemModifierSO> itemModifiers;
    [SerializeField] private List<GameObject> weaponPrefabs;
    [SerializeField] private List<GameObject> shieldPrefabs;

 
    public List<ItemModifierSO> GetRandomModifiers(int numberOfModifiers)
    {
        if (itemModifiers == null || itemModifiers.Count == 0) return new List<ItemModifierSO>();

        // Shuffle the list using Fisher-Yates algorithm
        List<ItemModifierSO> shuffledModifiers = new List<ItemModifierSO>(itemModifiers);
        for (int i = shuffledModifiers.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            ItemModifierSO temp = shuffledModifiers[i];
            shuffledModifiers[i] = shuffledModifiers[j];
            shuffledModifiers[j] = temp;
        }

        return shuffledModifiers.GetRange(0, Mathf.Clamp(numberOfModifiers, 1, shuffledModifiers.Count));
    }

    public GameObject GetRandomWeaponPrefab()
    {
        if (weaponPrefabs == null || weaponPrefabs.Count == 0) return null;
        return weaponPrefabs[Random.Range(0, weaponPrefabs.Count)];
    }

    public GameObject GetRandomShieldPrefab()
    {
        if (shieldPrefabs == null || shieldPrefabs.Count == 0) return null;
        return shieldPrefabs[Random.Range(0, shieldPrefabs.Count)];
    }
}