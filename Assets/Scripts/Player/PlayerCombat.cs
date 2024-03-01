using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    float attackRange = 2f;
    [SerializeField]
    int attackDamage= 10;
    [SerializeField]
    LayerMask enemyLayer;
    [SerializeField]
    Transform attackPosition;
    [SerializeField]
    WeaponController WeaponController;


    public void SimpleAttack()
    {
        
        Collider[] hitEnemies = Physics.OverlapSphere(attackPosition.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<IDamageable>().TakeDamage(attackDamage);
        }
    }
    public void SwingAttack()
    {
        WeaponController.TriggerSwingAttack();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPosition.position , attackRange);
    }
}

