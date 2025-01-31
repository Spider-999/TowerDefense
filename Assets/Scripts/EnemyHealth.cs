using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private int _increaseHealth = 1;
    [SerializeField] private Image _healthBar;
    private int _healthPercentImage = 10;
    private int _currentHealth = 0;
    [SerializeField] private int _towerDamage = 5;
    private Enemy _enemy;
    private TextSystem _textSystem;

    // This method is called when the object becomes enabled and active.
    private void OnEnable()
    {
        // Reset the health bar and health to full when the enemy is respawned
        _currentHealth = _maxHealth; 
        ResetHealthBar();
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _textSystem = FindObjectOfType<TextSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProjectileHit();
    }

    private void UpdateHealthBar()
    {
        _healthBar.fillAmount = (float)_currentHealth / _healthPercentImage;
    }

    private void ResetHealthBar()
    {
        _healthBar.fillAmount = 1;
    }

    private void ProjectileHit()
    {
        _currentHealth -= _towerDamage;
        UpdateHealthBar();

        if (_currentHealth < 0)
        {
            // Increase the health of the enemy when they spawn next time
            _maxHealth += _increaseHealth;

            // Update the killed enemies text when the enemy is killed
            _textSystem.UpdateEnemiesKilledText();

            // Instead of destroying the enemy, we can just disable it
            // and set it dormant in the object pool until it is needed again.
            gameObject.SetActive(false);

            // The player gets money when the enemy is killed
            _enemy.RewardCurrency();
        }
    }
}
