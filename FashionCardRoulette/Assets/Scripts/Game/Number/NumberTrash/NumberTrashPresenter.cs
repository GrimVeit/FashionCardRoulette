using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberTrashPresenter : INumberTrashEventsProvider
{
    private readonly NumberTrashModel _model;
    private readonly NumberTrashView _view;

    public NumberTrashPresenter(NumberTrashModel model, NumberTrashView view)
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
        _view.OnClickToTrash += _model.MoveToTrash;

        _model.OnMoveToTrash += _view.Close;
    }

    private void DeactivateEvents()
    {
        _view.OnClickToTrash -= _model.MoveToTrash;

        _model.OnMoveToTrash -= _view.Close;
    }

    #region Output

    public event Action OnMoveToTrash
    {
        add => _model.OnMoveToTrash += value;
        remove => _model.OnMoveToTrash -= value;
    }

    #endregion
}

public interface INumberTrashEventsProvider
{
    public event Action OnMoveToTrash;
}
