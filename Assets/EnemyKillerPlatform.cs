using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillerPlatform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(new AttackInfo(100000, false, true, this.gameObject));
            enemy.TakeDamage(new AttackInfo(100000, false, true, this.gameObject));
            enemy.TakeDamage(new AttackInfo(100000, false, true, this.gameObject));
        }
    }
}
