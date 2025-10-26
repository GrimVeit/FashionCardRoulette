using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDescriptionPresenter : IClaimEventsProvider
{
    private readonly TaskDescriptionModel _model;
    private readonly TaskDescriptionView _view;

    public TaskDescriptionPresenter(TaskDescriptionModel model, TaskDescriptionView view)
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
        _view.OnClaim += _model.Claim;

        _model.OnSetTask += _view.SetTask;
    }

    private void DeactivateEvents()
    {
        _view.OnClaim -= _model.Claim;

        _model.OnSetTask -= _view.SetTask;
    }

    #region Output

    public event Action OnClaimTask
    {
        add => _model.OnClaimTask += value;
        remove => _model.OnClaimTask -= value;
    }

    #endregion
}

public interface IClaimEventsProvider
{
    public event Action OnClaimTask;
}
