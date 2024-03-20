
using UnityEngine;

public interface IEnemyMoveable 
{
    Rigidbody mRigidbody { get; set; }
    void MoveEnemy(Vector3 Velocity);

}
