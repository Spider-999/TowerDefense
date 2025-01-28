using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currencyText;
    [SerializeField] private TextMeshProUGUI _enemiesKilledText;
    private int _killedEnemies = 0;

    public void UpdateCurrencyText(int currency)
    {
        _currencyText.text = $"CURRENCY {currency}";
    }

    public void UpdateEnemiesKilledText()
    {
        _killedEnemies++;
        _enemiesKilledText.text = $"ENEMIES KILLED {_killedEnemies}";
    }
}
