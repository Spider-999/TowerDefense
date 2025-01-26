using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField] private int _startCurrency = 100;
    [SerializeField] private int _currentCurrency;

    #region Properties
    public int CurrentCurrency
    {
        get => _currentCurrency;
    }
    #endregion

    private void Awake()
    {
        _currentCurrency = _startCurrency;
    }

    public void AddCurrency(int amount)
    {
        if (amount < 0)
        {
            Debug.LogError("Cannot add negative currency");
            return;
        }

        _currentCurrency += amount;
    }

    public void WithdrawCurrency(int amount)
    {
        if(amount <= 0)
        {
            Debug.LogError("Cannot withdraw negative or no currency");
            return;
        }

        if(amount > _currentCurrency)
        {
            Debug.LogError("Not enough currency to withdraw");
            return;
        }

        _currentCurrency -= amount;
    }
}
