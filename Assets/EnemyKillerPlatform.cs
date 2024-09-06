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
            enemy.TakeDamage(new AttackInfo(100000, DamageType.PHYSICAL, true, true, 0f, this.gameObject));
            enemy.TakeDamage(new AttackInfo(100000, DamageType.PHYSICAL, true, true, 0f, this.gameObject));
            enemy.TakeDamage(new AttackInfo(100000, DamageType.PHYSICAL, true, true, 0f, this.gameObject)); 
        }
    }
}
