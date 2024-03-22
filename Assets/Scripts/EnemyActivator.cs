using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyActivator : MonoBehaviour
{
    Enemy enemy;
    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        enemy.SetPlayerTarget(other.transform);
    }


    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        enemy.SetPlayerToNull();
    }
    public void CustomDashEvent(int aDashForce)
    {
        if (enemy != null)
        {
            enemy.DashForward(aDashForce);
        }
    }

}
