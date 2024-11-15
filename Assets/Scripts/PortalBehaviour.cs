
using System;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    Animator animator;
    PlayerController player;
    public bool CanTeleport;
    public bool CloseAndDisable = false;
    public FMODUnity.EventReference PortalSound;
    public Transform SpawnPoint;
    [SerializeField] PortalBehaviour ExitPortal;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (CloseAndDisable && animator != null)
        {
           // animator.SetTrigger("CloseAndDisable");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!CanTeleport)
        {
            return;
        }
        player  = other.GetComponent<PlayerController>();
        if (player  != null && animator != null)
        {
            TeleportToExitPortal();
            //animator.SetTrigger("OpenPortal");
        }
    }
    public void TeleportToExitPortal()
    {
        if (player != null && ExitPortal != null)
        { 
            player.FadeToTeleport(ExitPortal.SpawnPoint.position);
            ClosePortal();
            ExitPortal.ClosePortal();
        } 
    }
    public void PlayPortalSoundEvent()
    {
        AudioManager.instance.PlayOneShot(PortalSound, transform.position);
    }
    public void LoadNextLevelEvent()
    {
        if(player != null)
        {
            player.LoadNextLevel();
        }
    }
    public void ClosePortal()
    { 
        if (animator != null)
        {
            animator.SetTrigger("ClosePortal");
            CanTeleport = false;
        }
    }

    internal void ActivatePortal()
    {
        if( animator != null)
        { 
             animator.SetTrigger("OpenPortal");
        } 
    }
    public void EnableTeleport()
    { 
        CanTeleport = true;
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
