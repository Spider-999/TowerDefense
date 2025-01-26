using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Currency : MonoBehaviour
{
    [SerializeField] private int _startCurrency = 100;
    [SerializeField] private int _currentCurrency;
    [SerializeField] private TextMeshProUGUI _currencyText;

    #region Properties
    public int CurrentCurrency
    {
        get => _currentCurrency;
    }
    #endregion

    private void Awake()
    {
        _currentCurrency = _startCurrency;
        UpdateCurrencyText();
    }


    private void UpdateCurrencyText()
    {
        _currencyText.text = $"Currency: {CurrentCurrency}";
    }

    public void AddCurrency(int amount)
    {
        if (amount <= 0)
        {
            Debug.Log("Cannot add negative or no currency");
            return;
        }

        _currentCurrency += amount;
        UpdateCurrencyText();
    }

    public void WithdrawCurrency(int amount)
    {
        if(amount <= 0)
        {
            Debug.Log("Cannot withdraw negative or no currency");
            return;
        }

        if(amount > _currentCurrency)
        {
            Debug.Log("Not enough currency to withdraw");
            return;
        }

        _currentCurrency -= amount;
        UpdateCurrencyText();
    }
}
