using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteStatePresenter : IRouletteStateProvider
{
    private readonly RouletteStateModel _model;
    private readonly RouletteStateView _view;

    public RouletteStatePresenter(RouletteStateModel model, RouletteStateView view)
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
        _model.OnSetIdle += _view.SetIdle;
        _model.OnSetGame += _view.SetGame;
    }

    private void DeactivateEvents()
    {
        _model.OnSetIdle -= _view.SetIdle;
        _model.OnSetGame -= _view.SetGame;
    }

    #region Input

    public void SetGame() => _model.SetGame();
    public void SetIdle() => _model.SetIddle();

    #endregion
}

public interface IRouletteStateProvider
{
    public void SetGame();
    public void SetIdle();
}
