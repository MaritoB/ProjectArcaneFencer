
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileSpawner : MonoBehaviour
{
    private ObjectPool<ProjectileBehaviour> ProjectilePool;
    [SerializeField]
    OnHitEffectSOBase OnHitEffect;
    [SerializeField]
    AfterHitEffectSOBase AfterHitEffect; 
    [SerializeField]
    private ProjectileBehaviour ProjectilePrefab;
    [SerializeField]
    public Transform ShootPosition;
    [SerializeField]
    private float CastRate;
    private float CurrentCastRate;

    public void SetupProjectilePool( OnHitEffectSOBase aOnHitEffectSOBase, AfterHitEffectSOBase afterHitEffectSOBase)
    {
        OnHitEffect = aOnHitEffectSOBase; 
        AfterHitEffect = afterHitEffectSOBase;
        ProjectilePool = new ObjectPool<ProjectileBehaviour>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, 5, 10); 
    } 
 

    private void Update()
    {
        CurrentCastRate -= Time.deltaTime; 
    }
    private void Awake()
    {
        ProjectilePool = new ObjectPool<ProjectileBehaviour>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, 5, 10);
    }

    private void OnDestroyObject(ProjectileBehaviour Instance)
    {
        Destroy(Instance.gameObject);
    }

    ProjectileBehaviour CreatePooledObject()
    {
        ProjectileBehaviour instance = Instantiate(ProjectilePrefab, Vector3.zero, Quaternion.identity);
        instance.SetOnHitEffect(OnHitEffect);
        instance.SetAfterHitEffect(Instantiate(AfterHitEffect));
        instance.Disable += ReturnObjectToPool;
        instance.gameObject.SetActive(false);
        return instance;
    }

    private void OnReturnToPool(ProjectileBehaviour Instance)
    {
        Instance.gameObject.SetActive(false);

    }
    private void ReturnObjectToPool(ProjectileBehaviour Instance)
    {
        ProjectilePool.Release(Instance);
    }
    private void OnTakeFromPool(ProjectileBehaviour Instance)
    {
        Instance.gameObject.SetActive(true);
        Instance.ActivateProjectile();
        SpawnProjectile(Instance);

    }

    private void SpawnProjectile(ProjectileBehaviour instance)
    {
    }
    public void ShootProjectileForwardFromPool()
    {
        ProjectilePool.Get().Fire(ShootPosition.forward, ShootPosition.position);
    }
    public void ShootProjectileToDirectionFromPool(Vector3 aDirection, Vector3 aPosition)
    {
        ProjectilePool.Get().Fire(aDirection, aPosition);
    }
}
