using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFader : MonoBehaviour
{
    private ObjectFader _fader;
    [SerializeField] 
    GameObject Player;
    void Update()
    {
        if(Player == null)
        {
            return;

        }
        Vector3 direction = Player.transform.position - transform.position;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider == null)
            {
                return;
            }
            if (hit.collider.gameObject == Player)
            {
                if (_fader != null)
                {
                    _fader.DoFade = false;
                }
            }
            else
            {
                if (_fader != null)
                {
                    _fader.DoFade = false;
                }
                _fader = hit.collider.gameObject.GetComponent<ObjectFader>();
                if (_fader != null)
                {
                    _fader.enabled = true;
                    Debug.Log("Fade " + hit.collider.name);
                    _fader.DoFade = true;
                }
            }
        }
    }
}
