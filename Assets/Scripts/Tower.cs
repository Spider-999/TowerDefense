using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Currency _currency;
    [SerializeField] private int _towerCost = 50;
    public bool PlaceTower(Tower tower, Vector3 location)
    {
        _currency = FindObjectOfType<Currency>();

        // If the currency doesn't exist then log an error
        // and return false.
        if (_currency == null)
        {
            Debug.Log("Currency doesn't exist");
            return false;
        }


        // If the player doesn't have enough currency to place the tower
        // then log an error and return false.
        if (_currency.CurrentCurrency < _towerCost)
        {
            Debug.Log("Not enough currency to place tower");
            return false;
        }

        // Spawn the enemy at the specified location
        // and withdraw the currency from the player based
        // on the tower's cost.
        Instantiate(tower, location, Quaternion.identity);
        _currency.WithdrawCurrency(_towerCost);
        return true;
    }
}
