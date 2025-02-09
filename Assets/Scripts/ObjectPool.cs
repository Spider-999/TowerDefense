using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    // Limit the enemy pool size to a range between 5 and 50
    [SerializeField] [Range(0, 20)] private int _enemyPoolSize = 5;
    // Limit the spawn rate to a range between 0.5 and 5 seconds
    [SerializeField] [Range(0.5f, 5f)] private float _spawnRate = 1.5f;
    private GameObject[] _enemyPool;

    private void Awake()
    {
        PopulateEnemyPool();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            // Enable the enemies in the pool
            EnableEnemyInPool();
            // Wait for the spawn rate before spawning the next enemy
            yield return new WaitForSeconds(_spawnRate);
        }
    }


    private void EnableEnemyInPool()
    {
        for (int i = 0; i < _enemyPool.Length; i++)
        {
            // Check if the enemy is inactive
            if (!_enemyPool[i].activeInHierarchy)
            {
                // Set the enemy to active
                _enemyPool[i].SetActive(true);
                return;
            }
        }
    }

    private void PopulateEnemyPool()
    {
        // Initialize the enemy pool array
        _enemyPool = new GameObject[_enemyPoolSize];

        for (int i = 0; i < _enemyPool.Length; i++)
        {
            // Instantiate the enemy prefab
            _enemyPool[i] = Instantiate(_enemyPrefab, transform);

            // Set the enemy to inactive
            _enemyPool[i].SetActive(false);
        }
    }
}
