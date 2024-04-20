using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    MELEESKELETON,
    MELEEWITHSHIELDSKELETON,
    RANGEDSKELETON,
    RANGEDSTANDINGSKELETON,
    BOSSSKELETON
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
    List<EnemyType> EnemyTypes = new List<EnemyType>();
    public Dictionary<EnemyType, EnemyPoolInfo> EnemyPoolDictionary = new Dictionary<EnemyType, EnemyPoolInfo>();
    Transform CurrentRoomTransform;



    void Start()
    {
        EnemyTypes.Clear();
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
            EnemyTypes.Add(enemyPoolInfo.enemyType);
        }
        Debug.Log(EnemyTypes.ToString());
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
    public EnemyType GetRandomEnemy()
    {
        EnemyType newEnemy = EnemyTypes[Random.Range(0, EnemyTypes.Count)];
        if (newEnemy == EnemyType.BOSSSKELETON)
        {
            newEnemy = EnemyType.MELEESKELETON;
        }
        return newEnemy;
    }
    private Enemy GetEnemyFromPool(EnemyType aEnemyType)
    {
        if (EnemyPoolDictionary.TryGetValue(aEnemyType , out EnemyPoolInfo EnemyPool))
        {
            //busca inactivos
            for (int i = 0; i < EnemyPool.pool.Count; i++)
            {
                if (!EnemyPool.pool[i].gameObject.activeInHierarchy)
                {
                    return EnemyPool.pool[i];
                }
            }
            //busca activos muertos
            for (int i = 0; i < EnemyPool.pool.Count; i++)
            {
                if (!EnemyPool.pool[i].IsAlive)
                {
                    EnemyPool.pool[i].ResetStats();
                    return EnemyPool.pool[i];
                }
            }
            //crea nuevos si estan todos vivos y activos
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
    public void SpawnRandomEnemy()
    {
        SpawnEnemyFromPool(GetRandomEnemy(), CurrentRoomTransform);
    }
    public Enemy SpawnEnemyFromPool(EnemyType enemyType, Transform roomTransform)
    {
        Vector3 RandomSpawnPosition = GetValidSpawnPoint(roomTransform);
        Enemy enemy = GetEnemyFromPool(enemyType);
        if (enemy != null)
        {
            enemy.gameObject.SetActive(true);
            enemy.transform.position = RandomSpawnPosition;
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
        CurrentRoomTransform = roomTransform;
        Vector3 localMinBounds = -roomTransform.localScale / 2;
        Vector3 localMaxBounds = roomTransform.localScale / 2;
        Quaternion parentRotation = roomTransform.parent.rotation;
        Vector3 rotatedMinBounds = parentRotation * localMinBounds;
        Vector3 rotatedMaxBounds = parentRotation * localMaxBounds;
        Vector3 minBounds = roomTransform.position + rotatedMinBounds;
        Vector3 maxBounds = roomTransform.position + rotatedMaxBounds;
        Vector3 spawnPoint = new Vector3(
            Random.Range(minBounds.x, maxBounds.x),
            roomTransform.position.y,
            Random.Range(minBounds.z, maxBounds.z)
        );
        return spawnPoint;
    }
    private bool IsPositionValid(Vector3 position)
    {
        // Verificar si la posición está dentro de los límites de la habitación y no colisiona con ningún objeto
        Collider[] colliders = Physics.OverlapSphere(position, 0.5f);
        return colliders.Length == 0;
    }

}
