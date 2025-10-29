using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteSpinCountPresenter : IRouletteSpinCountProvider
{
    private readonly RouletteSpinCountModel _model;
    private readonly RouletteSpinCountView _view;

    public RouletteSpinCountPresenter(RouletteSpinCountModel model, RouletteSpinCountView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnChangeCountSpin += _view.SetCount;
        _model.OnEndSpins += _view.CloseSpin;
    }

    private void DeactivateEvents()
    {
        _model.OnChangeCountSpin -= _view.SetCount;
        _model.OnEndSpins -= _view.CloseSpin;
    }

    #region Input

    public void RemoveSpin() => _model.RemoveSpin();

    #endregion
}

public interface IRouletteSpinCountProvider
{
    public void RemoveSpin();
}
