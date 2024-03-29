using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    Animator animator;
    PlayerController player;
    public bool CloseAndDisable = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (CloseAndDisable && animator != null)
        {
            animator.SetTrigger("CloseAndDisable");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        player  = other.GetComponent<PlayerController>();
        if (player  != null && animator != null)
        {
            animator.SetTrigger("OpenPortal");
        }
    }
    public void LoadNextLevelEvent()
    {
        if(player != null)
        {
            player.LoadNextLevel();
        }
    }
        /*
    private void OnTriggerExit(Collider other)
    {
      
        player = null;
        if(animator != null)
        {
            animator.SetTrigger("ClosePortal");
        }
    }
      */
}
