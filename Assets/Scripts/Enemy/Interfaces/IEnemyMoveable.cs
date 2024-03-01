
using UnityEngine;

public interface IEnemyMoveable 
{
    Rigidbody Rigidbody { get; set; }
    void MoveEnemy(Vector3 Velocity);

}
