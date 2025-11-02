using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSelectionPresenter : INumberSelectionEventsProvider, INumberSelectionActivatorProvider
{
    private readonly NumberSelectionModel _model;
    private readonly NumberSelectionView _view;

    public NumberSelectionPresenter(NumberSelectionModel model, NumberSelectionView view)
    {
        _model = model; _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChooseSection += _model.SelectNumbers;

        _model.OnActivate += _view.Activate;
        _model.OnDeactivate += _view.Deactivate;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseSection -= _model.SelectNumbers;

        _model.OnActivate -= _view.Activate;
        _model.OnDeactivate -= _view.Deactivate;
    }

    #region Output

    public event Action<List<int>> OnChooseFiveNumbers { 
        add => _model.OnSelectFiveNumbers += value; 
        remove => _model.OnSelectFiveNumbers -= value; }

    #endregion

    #region Input

    public void Activate() => _model.ActivateChoose();
    public void Deactivate() => _model.DeactivateChoose();

    #endregion
}
public interface INumberSelectionActivatorProvider
{
    public void Activate();
    public void Deactivate();
}

public interface INumberSelectionEventsProvider
{
    public event Action<List<int>> OnChooseFiveNumbers;
}
