using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Currency : MonoBehaviour
{
    [SerializeField] private int _startCurrency = 100;
    [SerializeField] private int _currentCurrency;
    [SerializeField] private TextMeshProUGUI _currencyText;
    private TextSystem _textSystem;

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

    private void Start()
    {
        _textSystem = FindObjectOfType<TextSystem>();
        _textSystem.UpdateCurrencyText(_currentCurrency);
    }

    public void AddCurrency(int amount)
    {
        if (amount <= 0)
        {
            Debug.Log("Cannot add negative or no currency");
            return;
        }

        _currentCurrency += amount;
        _textSystem.UpdateCurrencyText(_currentCurrency);
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
        _textSystem.UpdateCurrencyText(_currentCurrency);
    }
}
