using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemy : MonoBehaviour
{
    private Transform _enemyTarget;
    
    void Start()
    {
        // Find the enemy object
        _enemyTarget = FindObjectOfType<EnemyMovement>().transform;
    }

    void Update()
    {
        AimTower();
    }

    private void AimTower()
    {
        // Rotate the cannon to face the enemy
        transform.LookAt(_enemyTarget);
    }
}
