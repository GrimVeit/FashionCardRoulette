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
        _model.OnSetIdle_Smooth += _view.SetIdle_Smooth;
        _model.OnSetGame_Smooth += _view.SetGame_Smooth;
    }

    private void DeactivateEvents()
    {
        _model.OnSetIdle -= _view.SetIdle;
        _model.OnSetIdle_Smooth -= _view.SetIdle_Smooth;
        _model.OnSetGame_Smooth -= _view.SetGame_Smooth;
    }

    #region Input

    public void SetGame_Smooth() => _model.SetGame_Smooth();
    public void SetIdle_Smooth() => _model.SetIdle_Smooth();
    public void SetIdle() => _model.SetIddle();

    #endregion
}

public interface IRouletteStateProvider
{
    public void SetGame_Smooth();
    public void SetIdle_Smooth();
    public void SetIdle();
}
