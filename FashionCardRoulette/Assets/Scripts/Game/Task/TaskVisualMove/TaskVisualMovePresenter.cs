using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskVisualMovePresenter : ITaskVisualMoveProvider
{
    private readonly TaskVisualMoveModel _model;
    private readonly TaskVisualMoveView _view;

    public TaskVisualMovePresenter(TaskVisualMoveModel model, TaskVisualMoveView view)
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
        _model.OnMoveFinish += _view.SetFinish;
    }

    private void DeactivateEvents()
    {
        _model.OnMoveFinish -= _view.SetFinish;
    }

    #region Input

    public void MoveFinish() => _model.MoveFinish();

    #endregion
}

public interface ITaskVisualMoveProvider
{
    void MoveFinish();
}
