using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFleeCheck : MonoBehaviour
{
   public GameObject PlayerTarget { get; set; }
    private Enemy _enemy;
    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _enemy.SetWithinFleeDistanceBool(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _enemy.SetWithinFleeDistanceBool(false);
         
    }
}
