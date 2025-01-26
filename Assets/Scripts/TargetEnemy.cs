using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemy : MonoBehaviour
{
    private Transform _enemyTarget;
    [SerializeField] ParticleSystem _projectileParticle;
    [SerializeField] private float _towerRange = 20f;

    private void Update()
    {
        FindClosestEnemy();
        AimTower();
    }

    private void FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            // Find the distance from the tower to the enemy
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            // Compare the distance to the shortest distance
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }

        _enemyTarget = closestEnemy;
    }

    private void AimTower()
    {
        float enemyDistance = Vector3.Distance(transform.position, _enemyTarget.position);

        // If the enemy is not in the range of the tower
        // do nothing.
        if(enemyDistance > _towerRange)
        {
            _enemyTarget = null;
            Attack(false);
            return;
        }

        // Rotate the cannon to face the enemy
        transform.LookAt(_enemyTarget);
        Attack(true);
    }

    private void Attack(bool isActive)
    {
        var emission = _projectileParticle.emission;
        emission.enabled = isActive;
    }
}
