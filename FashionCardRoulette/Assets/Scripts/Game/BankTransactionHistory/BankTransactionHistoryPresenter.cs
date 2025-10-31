using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankTransactionHistoryPresenter
{
    private readonly BankTransactionHistoryModel _model;
    private readonly BankTransactionHistoryView _view;

    public BankTransactionHistoryPresenter(BankTransactionHistoryModel model, BankTransactionHistoryView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnSetCoinsEarn += _view.SetCoinsEarn;
        _model.OnSetCoinsSpent += _view.SetCoinsSpent;
        _model.OnSetCoinsTotal += _view.SetCoinsTotal;
    }

    private void DeactivateEvents()
    {
        _model.OnSetCoinsEarn -= _view.SetCoinsEarn;
        _model.OnSetCoinsSpent -= _view.SetCoinsSpent;
        _model.OnSetCoinsTotal -= _view.SetCoinsTotal;
    }
}
