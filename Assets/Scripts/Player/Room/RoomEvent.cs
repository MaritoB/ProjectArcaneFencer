using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Horde
{
    public List<EnemyType> enemies;

}

public class RoomEvent : MonoBehaviour
{

    public List<Door> doors;
    bool isEventPerformed = false;
    public Transform spawnArea;
    public bool SpawnMelee;
    public bool SpawnRanged;
    //public List<List<EnemyType>> Hordes = new List<List<EnemyType>>();

    [Serialize] public List<Horde> Hordes;
    public int enemysRemaining;
    public int currentHorde = 0;


    // Start is called before the first frame update
    void Start()
    {
        /*
        Hordes.Clear();
        List<EnemyType> newHorde = new List<EnemyType> { EnemyType.MELEESKELETON, EnemyType.RANGEDSKELETON };
        Hordes.Add(newHorde);
        newHorde = new List<EnemyType> { EnemyType.MELEESKELETON, EnemyType.MELEESKELETON, EnemyType.RANGEDSKELETON, EnemyType.RANGEDSKELETON };
        Hordes.Add(newHorde);
        newHorde = new List<EnemyType> { EnemyType.MELEESKELETON, EnemyType.MELEESKELETON, EnemyType.RANGEDSKELETON, EnemyType.RANGEDSKELETON, EnemyType.MELEESKELETON, EnemyType.RANGEDSKELETON };
        Hordes.Add(newHorde);
         */
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void CloseAllDoors()
    {
        foreach (Door door in doors)
        {
            door.Close();
        }
    }
    public void OpenAllDoors()
    {
        foreach (Door door in doors)
        {
            door.Open();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;
        if (isEventPerformed)
        {
            return;
        }
        isEventPerformed = true;
        SpawnHorde(Hordes[0]);
        CloseAllDoors();
    }
    public void SpawnEnemy(EnemyType enemyType)
    {
        Enemy enemy = EnemySpawner.Instance.SpawnEnemyFromPool(enemyType, spawnArea);
        enemy.SetOwner(this);
    }
    public void SpawnHorde(Horde horde)
    {
        foreach (EnemyType enemy in horde.enemies)
        {
            SpawnEnemy(enemy);
        }
        enemysRemaining = horde.enemies.Count;
        currentHorde++;
    }

    public void InformEnemyDeath()
    {
        --enemysRemaining;
        if (enemysRemaining <= 0)
        {
            if (currentHorde < Hordes.Count)
            {
                SpawnHorde(Hordes[currentHorde]);
            }
            else
            {
                OpenAllDoors();
            }
        }
    }
}
