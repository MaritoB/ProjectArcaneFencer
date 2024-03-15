using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum EnemyType
{
    MELEESKELETON,
    RANGEDSKELETON
}

[System.Serializable]
public class EnemyPoolInfo
{
    public EnemyType enemyType;
    public GameObject prefab;
    public int poolSize = 10;
    public List<Enemy> pool = new List<Enemy>();

}

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    public List<EnemyPoolInfo> EnemyPoolList;
   public Dictionary<EnemyType, EnemyPoolInfo> EnemyPoolDictionary = new Dictionary<EnemyType, EnemyPoolInfo>();
    
    void Start()
    {
        foreach (EnemyPoolInfo enemyPoolInfo in EnemyPoolList)
        {
            enemyPoolInfo.pool.Clear();
            enemyPoolInfo.pool.Capacity = enemyPoolInfo.poolSize;
            //EnemyPoolDictionary.TryGetValue(enemyPoolInfo.enemyType, out EnemyPoolInfo enemyPool);

            // Pre-instantiate enemies and add them to the pool
            for (int i = 0; i < enemyPoolInfo.poolSize; i++)
            {
                GameObject enemyGO = Instantiate(enemyPoolInfo.prefab,Vector3.zero, Quaternion.identity);
                Enemy enemy = enemyGO.GetComponent<Enemy>();    
                enemy.gameObject.SetActive(false);
                enemyPoolInfo.pool.Add(enemy);
            }
            EnemyPoolDictionary.Add(enemyPoolInfo.enemyType, enemyPoolInfo);
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private Enemy GetEnemyFromPool(EnemyType aEnemyType)
    {
        if (EnemyPoolDictionary.TryGetValue(aEnemyType , out EnemyPoolInfo EnemyPool))
        {
            for (int i = 0; i < EnemyPool.pool.Count; i++)
            {
                if (!EnemyPool.pool[i].gameObject.activeInHierarchy)
                {
                    return EnemyPool.pool[i];
                }
            }
            GameObject enemyGO = Instantiate(EnemyPool.prefab, Vector3.zero, Quaternion.identity);
            Enemy enemy = enemyGO.GetComponent<Enemy>();
            EnemyPool.pool.Add(enemy);
            return enemy;
        }
        else
        {
            Debug.LogError("Prefab not found in the enemy pools.");
            return null;
        }
    }

    public Enemy SpawnEnemyFromPool(EnemyType enemyType, Transform roomTransform)
    {
        Vector3 RandomSpawnPosition = GetValidSpawnPoint(roomTransform);
        Enemy enemy = GetEnemyFromPool(enemyType);
        if (enemy != null)
        {
            enemy.gameObject.SetActive(true);
            enemy.transform.position = RandomSpawnPosition;
            enemy.ResetStats();
        }
        return enemy;
    }
    private  Vector3 GetValidSpawnPoint(Transform roomTransform)
    {
        Vector3 spawnPoint = Vector3.zero;
        bool validPositionFound = false;
        int attempts = 0;
        int maxAttempts = 10;

        while (!validPositionFound && attempts < maxAttempts)
        {
            spawnPoint = GenerateRandomPositionInsideRoom(roomTransform);
            validPositionFound = IsPositionValid(spawnPoint);
            attempts++;
        }
        return spawnPoint;
    }
    private Vector3 GenerateRandomPositionInsideRoom(Transform roomTransform)
    {
        // Obtener los límites de la habitación
        Vector3 minBounds = roomTransform.position - roomTransform.localScale / 2;
        Vector3 maxBounds = roomTransform.position + roomTransform.localScale / 2;

        // Generar una posición aleatoria dentro de los límites de la habitación
        Vector3 spawnPoint = new Vector3(Random.Range(minBounds.x, maxBounds.x), roomTransform.position.y, Random.Range(minBounds.z, maxBounds.z));

        return spawnPoint;
    }
    private bool IsPositionValid(Vector3 position)
    {
        // Verificar si la posición está dentro de los límites de la habitación y no colisiona con ningún objeto
        Collider[] colliders = Physics.OverlapSphere(position, 0.5f);
        return colliders.Length == 0;
    }

}
