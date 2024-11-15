using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CotoutObject : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;
    [SerializeField]
    private float _CutoutSize;
    [SerializeField]
    private float _FalloffSize;

    [SerializeField]
    private LayerMask wallMask;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>(); 
    }

    void Update()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);
        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);
        for (int i = 0; i< hitObjects.Length; ++i)
        {
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;
            for (int m = 0; m< materials.Length; ++m)
            {
                materials[m].SetVector("_CutoutPosition", cutoutPos);
                materials[m].SetFloat("_CutoutSize", _CutoutSize);
                materials[m].SetFloat("_FalloffSize", _FalloffSize);
            }
        }
        
    }
}
