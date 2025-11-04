using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorArrowPresenter : ISectorArrowProvider, ISectorArrowEventsProvider
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
        _view.OnSectorZoneChanged += _model.SetSectorZone;



        _view.OnClickToZone += _model.DeactivateZone;

        _model.OnActivateZone += _view.ActivateZone;
        _model.OnDeactivateZone += _view.DeactivateZone;
    }

    private void DeactivateEvents()
    {
        _view.OnSectorZoneChanged -= _model.SetSectorZone;



        _view.OnClickToZone -= _model.DeactivateZone;

        _model.OnActivateZone -= _view.ActivateZone;
        _model.OnDeactivateZone -= _view.DeactivateZone;
    }

    #region Output

    public event Action OnActivateZone { add => _model.OnActivateZone += value; remove => _model.OnActivateZone -= value; }
    public event Action OnDeactivateZone { add => _model.OnDeactivateZone += value; remove => _model.OnDeactivateZone -= value; }

    #endregion

    #region Input

    public void ActivateZone() => _model.ActivateZone();

    #endregion
}

public interface ISectorArrowProvider
{
    void ActivateZone();
}

public interface ISectorArrowEventsProvider
{
    public event Action OnActivateZone;
    public event Action OnDeactivateZone;
}
