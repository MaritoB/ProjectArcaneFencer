using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PSMover : MonoBehaviour
{
    Transform TargetTransform;
    Transform OriginPosition;
    ParticleSystem SoulsPS;
    [SerializeField] float LerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        SoulsPS = GetComponent<ParticleSystem>();
        OriginPosition = GetComponentInParent<Transform>();
    }
    public void SetDestination(Transform aTarget)
    {
        TargetTransform = aTarget;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(TargetTransform == null)
        {
            return;
        }
        transform.position = Vector3.Lerp(transform.position,TargetTransform.position,LerpSpeed * Time.fixedDeltaTime);
        if((transform.position - TargetTransform.position).magnitude < 1f)
        {
            SoulsPS.Stop();
            transform.position = OriginPosition.position;
            TargetTransform = null;
        }
    }

    internal void Reset()
    {
        transform.position = OriginPosition.position;
        SoulsPS.Play();
    }
}
