using System;
using Unity.VisualScripting;
using UnityEngine;

public class InfiniteRoomEvent : MonoBehaviour, IOwner
{
    public Transform spawnArea;
    public Transform BossSpawnArea;
    //public List<List<EnemyType>> Hordes = new List<List<EnemyType>>();
    public Horde InfiniteHorde, BossHorde;
    public int enemysRemaining;
    public int CurrentLevel = 0;
    public PlayerController playerController;
    public Transform BossCamera;
    public Transform FocusPoint;
    // Start is called before the first frame update
    void Start()
    {
        InfiniteHorde.enemies.Clear();
        InfiniteHorde.enemies.Add(EnemyType.MELEESKELETON);
        SpawnHorde(InfiniteHorde);
 
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
    public void SpawnBoss(EnemyType enemyType)
    {
        Enemy enemy = EnemySpawner.Instance.SpawnEnemyFromPool(enemyType, BossSpawnArea);
        enemy.SetLevel(CurrentLevel);        
        enemy.SetOwner(this);
        enemy.TriggerRiseAnimation();
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
    public void SpawnBossHorde(Horde horde)
    {
        CurrentLevel++;
        AudioManager.instance.PlayBossMusic();
        foreach (EnemyType enemy in horde.enemies)
        {
            SpawnBoss(enemy);
        }
        BossCamera.gameObject.SetActive(true);
        enemysRemaining = horde.enemies.Count;
    }

    public void InformEnemyDeath()
    {
        --enemysRemaining;
        if (enemysRemaining <= 0)
        {
            if ((CurrentLevel % 2) == 0)
            {
                InfiniteHorde.enemies.Add(EnemySpawner.Instance.GetRandomEnemy());
            }
            if((CurrentLevel>5)&&((CurrentLevel-1)% 5 )== 0){

                playerController.LevelUP();
                BossCamera.gameObject.SetActive(false);
                AudioManager.instance.FinishBossMusic();
            }
            if ((CurrentLevel % 5) == 0)
            {
                BossHorde.enemies.Add(EnemyType.BOSSSKELETON);
                SpawnBossHorde(BossHorde);
                return;
            }
            SpawnHorde(InfiniteHorde);
        }
    }
}
