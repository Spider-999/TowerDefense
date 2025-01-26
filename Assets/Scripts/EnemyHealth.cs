using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private int _currentHealth = 0;

    // This method is called when the object becomes enabled and active.
    void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProjectileHit();
    }

    private void ProjectileHit()
    {
        _currentHealth -= 2;
        if (_currentHealth < 0)
        {
            // Instead of destroying the enemy, we can just disable it
            // and set it dormant in the object pool until it is needed again.
            gameObject.SetActive(false);
        }
    }
}
