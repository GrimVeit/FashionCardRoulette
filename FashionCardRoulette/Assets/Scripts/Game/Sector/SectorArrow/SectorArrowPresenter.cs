using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorArrowPresenter : ISectorArrowProvider
{
    private readonly SectorArrowModel _model;
    private readonly SectorArrowView _view;

    public SectorArrowPresenter(SectorArrowModel model, SectorArrowView view)
    {
        _model = model;
        _view = view;
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
        _model.OnActivateArrowMove += _view.StartMoveArrow;
        _model.OnDeactivateArrowMove += _view.StopMoveArrow;
    }

    private void DeactivateEvents()
    {
        _model.OnActivateArrowMove -= _view.StartMoveArrow;
        _model.OnDeactivateArrowMove -= _view.StopMoveArrow;
    }

    #region Input

    public void ActivateMove() => _model.ActivateArrowMove();

    #endregion
}

public interface ISectorArrowProvider
{
    void ActivateMove();
}
