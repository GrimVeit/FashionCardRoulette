using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskVisualPresenter : ITaskVisualProvider, ITaskVisualEventsProvider
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

        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChooseCell += _model.ChooseCell;
        _view.OnChooseTask += _model.ChooseTask;

        _model.OnActivateCells += _view.ActivateCells;
        _model.OnDeactivateCells += _view.DeactivateCells;
        _model.OnSetTaskConditions += _view.SetTasks;
        _model.OnChooseCell_Value += _view.SetNumberValue;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseCell -= _model.ChooseCell;
        _view.OnChooseTask -= _model.ChooseTask;

        _model.OnActivateCells -= _view.ActivateCells;
        _model.OnDeactivateCells -= _view.DeactivateCells;
        _model.OnSetTaskConditions -= _view.SetTasks;
        _model.OnChooseCell_Value -= _view.SetNumberValue;
    }

    #region Output

    public event Action OnChooseCell
    {
        add => _model.OnChooseCell += value;
        remove => _model.OnChooseCell -= value;
    }

    public event Action OnChooseTask
    {
        add => _model.OnChooseTask += value;
        remove => _model.OnChooseTask -= value;
    }

    public event Action<(TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition)> OnChooseTask_Value
    {
        add => _model.OnChooseTask_Value += value;
        remove => _model.OnChooseTask_Value -= value;
    }

    #endregion

    #region Input

    public void SetRandomTasks()
    {
        _model.SetRandomTasks();
    }

    public void ActivateCells()
    {
        _model.ActivateCells();
    }

    public void DeactivateCells()
    {
        _model.DeactivateCells();
    }

    #endregion
}

public interface ITaskVisualProvider
{
    public void SetRandomTasks();
    public void ActivateCells();
    public void DeactivateCells();
}

public interface ITaskVisualEventsProvider
{
    public event Action OnChooseCell;

    public event Action<(TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition)> OnChooseTask_Value;
    public event Action OnChooseTask;
}
