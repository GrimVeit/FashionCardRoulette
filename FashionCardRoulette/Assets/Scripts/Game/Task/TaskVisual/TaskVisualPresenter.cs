using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskVisualPresenter
{
    private readonly TaskVisualModel _model;
    private readonly TaskVisualView _view;

    public TaskVisualPresenter(TaskVisualModel model, TaskVisualView view)
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
        _model.OnResetTasks += _view.ResetTasks;
    }

    private void DeactivateEvents()
    {
        _model.OnResetTasks -= _view.ResetTasks;
    }

    #region Input

    public void ResetTasks()
    {
        _model.ResetTasks();
    }

    #endregion
}
