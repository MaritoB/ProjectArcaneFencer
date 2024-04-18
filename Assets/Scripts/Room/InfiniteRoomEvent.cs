using System;
using Unity.VisualScripting;
using UnityEngine;

public class InfiniteRoomEvent : MonoBehaviour, IOwner
{
    public Transform spawnArea;
    //public List<List<EnemyType>> Hordes = new List<List<EnemyType>>();
    public Horde InfiniteHorde;
    public int enemysRemaining;
    public int CurrentLevel = 0;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        InfiniteHorde.enemies.Clear();
        InfiniteHorde.enemies.Add(EnemyType.MELEESKELETON);
        SpawnHorde(InfiniteHorde);
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
   
    public void SpawnEnemy(EnemyType enemyType)
    {
        Enemy enemy = EnemySpawner.Instance.SpawnEnemyFromPool(enemyType, spawnArea);
        enemy.SetLevel(CurrentLevel);
        enemy.SetOwner(this);
    }
    public void SpawnHorde(Horde horde)
    {
        CurrentLevel++;
        foreach (EnemyType enemy in horde.enemies)
        {
            SpawnEnemy(enemy);
        }
        enemysRemaining = horde.enemies.Count;
}

    public void InformEnemyDeath()
    {
        --enemysRemaining;
        if (enemysRemaining <= 0)
        {
            playerController.LevelUP();
            if((CurrentLevel % 2) == 0)
            {
                InfiniteHorde.enemies.Add(EnemySpawner.Instance.GetRandomEnemy());

            }
            SpawnHorde(InfiniteHorde);
        }
    }
}
