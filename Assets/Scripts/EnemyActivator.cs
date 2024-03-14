using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


}
