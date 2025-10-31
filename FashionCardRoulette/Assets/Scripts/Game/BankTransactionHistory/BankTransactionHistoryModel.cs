using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankTransactionHistoryModel
{
    private int coinsEarn = 0;
    private int coinsSpent = 0;

    private int coinsTotal = 0;

    private readonly IMoneyEventsProvider _moneyEventsProvider;

    public BankTransactionHistoryModel(IMoneyEventsProvider moneyEventsProvider)
    {
        _moneyEventsProvider = moneyEventsProvider;

        _moneyEventsProvider.OnSendMoney += SendMoney;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _moneyEventsProvider.OnSendMoney -= SendMoney;
    }

    private void SendMoney(int money)
    {
        if(money > 0)
        {
            coinsEarn += money;
        }
        else
        {
            money = -money;
            coinsSpent += money;
        }

        coinsTotal = coinsEarn - coinsSpent;

        OnSetCoinsEarn?.Invoke(coinsEarn);
        OnSetCoinsSpent?.Invoke(coinsSpent);
        OnSetCoinsTotal?.Invoke(coinsTotal);
    }

    #region Output

    public event Action<int> OnSetCoinsEarn;
    public event Action<int> OnSetCoinsSpent;
    public event Action<int> OnSetCoinsTotal;

    #endregion
}
