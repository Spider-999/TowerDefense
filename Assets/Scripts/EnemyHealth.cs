using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private int _currentHealth = 0;

    // Start is called before the first frame update
    void Start()
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
        if( _currentHealth < 0 )
            // Destroy the gameObject that has this script
            Destroy(gameObject);
    }
}
