using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    [SerializeField]
    float fadeSpeed, fadeAmount;
    float[] originalOpacity;
    Material[] materials;
    public bool DoFade = false;
    // Start is called before the first frame update
    void Start()
    {
        int size = GetComponent<MeshRenderer>().materials.Length;
        materials = new Material[size];
        originalOpacity = new float[size];
        materials = GetComponent<MeshRenderer>().materials;
        for (int i = 0; i < materials.Length; i++)
        { 
            originalOpacity[i] = materials[i].color.a;
        } 

    }

    // Update is called once per frame
    void Update()
    {
        if (DoFade)
        {
            Fade();
        }
        else
        {
            ReseteFade(); 
        }
        
    }
    void Fade()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            Color currentColor = materials[i].color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));
            materials[i].color = smoothColor;
        }


    }
    void ReseteFade()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            if (originalOpacity[i] - materials[i].color.a < 0.01)
            {
                materials[i].color = Color.white;
                this.enabled = false;
                return;
            }
            Color currentColor = materials[i].color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalOpacity[i], fadeSpeed * Time.deltaTime));
            materials[i].color = smoothColor;
        }

    }
}
