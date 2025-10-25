using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDescriptionPresenter
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
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnSetTask += _view.SetTask;
    }

    private void DeactivateEvents()
    {
        _model.OnSetTask -= _view.SetTask;
    }
}
