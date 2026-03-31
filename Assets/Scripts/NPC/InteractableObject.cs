using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable{
    void Interact();
}
public class InteractableObject: MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    [SerializeField]
    SphereCollider InteractableArea; 
    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] PortalBehaviour portal;

    public void Interact()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerLayerMask == (playerLayerMask | (1 << other.gameObject.layer)))
        {
            portal.ActivatePortal(); 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (playerLayerMask == (playerLayerMask | (1 << other.gameObject.layer)))
        {
            Debug.Log("Bye");


        }
    }
}
