using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseNumberPresenter : IChooseNumberProvider, IChooseNumberEventsProvider
{
    private readonly ChooseNumberModel _model;
    private readonly ChooseNumberView _view;

    public ChooseNumberPresenter(ChooseNumberModel model, ChooseNumberView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _model.OnSetNumber_Value += _view.SetNumber;
    }

    private void DeactivateEvents()
    {
        _model.OnSetNumber_Value -= _view.SetNumber;
    }

    #region Output

    public event Action OnSetNumber
    {
        add => _model.OnSetNumber += value;
        remove => _model.OnSetNumber -= value;
    }

    #endregion

    #region Input

    public void SetNumber(NumberValue numberValue)
    {
        _model.SetNumber(numberValue);
    }

    #endregion
}

public interface IChooseNumberProvider
{
    void SetNumber(NumberValue numberValue);
}

public interface IChooseNumberEventsProvider
{
    public event Action OnSetNumber;
}
