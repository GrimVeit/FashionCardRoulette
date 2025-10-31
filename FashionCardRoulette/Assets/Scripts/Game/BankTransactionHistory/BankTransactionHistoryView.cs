using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BankTransactionHistoryView : View
{
    [SerializeField] private TextMeshProUGUI textCoinsEarn;
    [SerializeField] private TextMeshProUGUI textCoinsSpent;
    [SerializeField] private TextMeshProUGUI textCoinsTotal;

    public void SetCoinsEarn(int coins)
    {
        textCoinsEarn.text = coins.ToString();
    }

    public void SetCoinsSpent(int coins)
    {
        textCoinsSpent.text = coins.ToString();

    }

    public void SetCoinsTotal(int coins)
    {
        textCoinsTotal.text = coins.ToString();
    }
}
