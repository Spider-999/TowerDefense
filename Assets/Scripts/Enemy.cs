using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // The amount a player will receive when the enemy is killed
    [SerializeField] private int _currencyDropValue = 10;
    // The amount a player will lose when the enemy reaches the end
    [SerializeField] private int _currencyLoseValue = 10;
    private Currency _currency;

    // Start is called before the first frame update
    private void Start()
    {
        _currency = FindObjectOfType<Currency>();
    }

    public void RewardCurrency()
    {
        if (_currency == null)
        {
            Debug.LogError("Currency is null");
            return;
        }

        _currency.AddCurrency(_currencyDropValue);
    }

    public void LoseCurrency()
    {
        if (_currency == null)
        {
            Debug.LogError("Currency is null");
            return;
        }

        _currency.WithdrawCurrency(_currencyDropValue);
    }
}
