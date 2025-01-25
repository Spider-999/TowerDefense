using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnRate = 1.5f;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            // Instantiate the enemy prefab
            Instantiate(_enemyPrefab, transform);
            // Wait for the spawn rate before spawning the next enemy
            yield return new WaitForSeconds(_spawnRate);
        }
    }
}
