using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    IProjectileOnHitEffect OnHitEffect;
    IProjectileAfterHitEffect AfterHitEffect;
    // Start is called before the first frame update
    [SerializeField] float _baseProjectilSpeed;
    [SerializeField] float _lifeTime;
    [SerializeField]
    ParticleSystem OnHitParticleSystem;
    [SerializeField]
    ParticleSystem ProjectileParticleSystem;
    Rigidbody _rigidBody;
    MeshRenderer _meshRenderer;
    SphereCollider _sphereCollider;
    Vector3 _direction;
    public delegate void OnDisableCallback(ProjectileBehaviour instance);
    public OnDisableCallback Disable;
    float _currentLifeTime;
    public LayerMask TargetLayerMask, WallsLayerMask;
    public bool isReadyToDisable;
    public void SetOnHitEffect(IProjectileOnHitEffect aOnHitEffect)
    {
        OnHitEffect = aOnHitEffect;
    }
    public void SetAfterHitEffect(IProjectileAfterHitEffect aAfterHitEffect)
    {
        AfterHitEffect = aAfterHitEffect;
    }
    void Awake()
    {
        _currentLifeTime = _lifeTime;
        _rigidBody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _sphereCollider = GetComponent<SphereCollider>();
    }
    public Vector3 GetDirection()
    {
        return _direction;
    }
    public void Fire(Vector3 aDirection, Vector3 OriginPosition)
    {
        _direction = aDirection;
        gameObject.SetActive(true);
        transform.position = OriginPosition; 
        _currentLifeTime = _lifeTime;
        _rigidBody.velocity = aDirection * _baseProjectilSpeed;
    }
    public void ChangeDirection(Vector3 aNewTargetPosition)
    {
        if(aNewTargetPosition == null) { return; }
        _direction = (aNewTargetPosition - transform.position).normalized;
        _rigidBody.velocity = _direction * _baseProjectilSpeed;
        _currentLifeTime = _lifeTime;

    }
    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + Vector3.right* Mathf.Sin(_currentLifeTime);
        _currentLifeTime -= Time.deltaTime;
        if (_currentLifeTime < 0)
        {
            Disable?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
    public void ActivateProjectile()
    {
        AfterHitEffect.ResetAfterHitEffect();
        gameObject.SetActive(true);
        _meshRenderer.enabled = true;
        _sphereCollider.enabled = true;
        _currentLifeTime = _lifeTime;
        isReadyToDisable = false;
        if (ProjectileParticleSystem != null)
        {
            ProjectileParticleSystem.Play();
            ProjectileParticleSystem.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (WallsLayerMask == (WallsLayerMask | (1 << other.gameObject.layer)))
        {
            OnHitParticleSystem.Emit(10);
            DisableProjectile();
            return;
        }
        if (TargetLayerMask == (TargetLayerMask | (1 << other.gameObject.layer)))
        {
            if(OnHitParticleSystem != null)
            {
                OnHitParticleSystem.Emit(10);
            }
            if (OnHitEffect != null)
            {
                OnHitEffect.OnHitEffect(other);
            }
            if(AfterHitEffect != null)
            {
                AfterHitEffect.AfterHitEffect(this);
            }
        }
    }
    public void DisableProjectile()
    {
        _meshRenderer.enabled = false;
        _sphereCollider.enabled = false;
        _rigidBody.velocity = Vector3.zero;
        isReadyToDisable = true;
        if(ProjectileParticleSystem != null)
        {
            ProjectileParticleSystem.Stop();
            ProjectileParticleSystem.gameObject.SetActive(false);
        }
    }
        /*
    private void OnCollisionEnter(Collision collision)
    {
        particleSystem.Emit(30);
        _meshRenderer.enabled = false;
        _sphereCollider.enabled = false;
        _rigidBody.velocity = Vector3.zero;
        if (EffectOnHit != null)
        {
            EffectOnHit.OnHitEffect(collision);
        }
    }
         */
    private void OnParticleSystemStopped()
    {
        if(isReadyToDisable) 
        {
            Disable?.Invoke(this);
        }
    }

}
