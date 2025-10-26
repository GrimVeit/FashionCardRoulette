using System;

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
        _view.OnSuccessTask += _model.SetClaimableTask;
        _view.OnFailTask += _model.SetFailedTask;


        _model.OnActivateInteractionTask += _view.ActivateInteractionTask;
        _model.OnDeactivateInteractionTask += _view.DeactivateInteractionTask;

        _model.OnActivateCells += _view.ActivateCells;
        _model.OnDeactivateCells += _view.DeactivateCells;

        _model.OnSetTaskConditions += _view.SetTasks;
        _model.OnChooseCell_Value += _view.SetNumberValue;

        _model.OnSetInProgressTask += _view.SetTaskInProgress;
        _model.OnSetClaimableTask += _view.SetTaskClaimable;
        _model.OnSetCompletedTask += _view.SetTaskCompleted;
        _model.OnSetFailedTask += _view.SetTaskFailed;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseCell -= _model.ChooseCell;
        _view.OnChooseTask -= _model.ChooseTask;
        _view.OnSuccessTask -= _model.SetClaimableTask;
        _view.OnFailTask -= _model.SetFailedTask;


        _model.OnActivateInteractionTask -= _view.ActivateInteractionTask;
        _model.OnDeactivateInteractionTask -= _view.DeactivateInteractionTask;

        _model.OnActivateCells -= _view.ActivateCells;
        _model.OnDeactivateCells -= _view.DeactivateCells;

        _model.OnSetTaskConditions -= _view.SetTasks;
        _model.OnChooseCell_Value -= _view.SetNumberValue;

        _model.OnSetInProgressTask -= _view.SetTaskInProgress;
        _model.OnSetClaimableTask -= _view.SetTaskClaimable;
        _model.OnSetCompletedTask -= _view.SetTaskCompleted;
        _model.OnSetFailedTask -= _view.SetTaskFailed;
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

    public event Action<(TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition, int TaskId)> OnChooseTask_Value
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


    public void ActivateInteractionTask()
    {
        _model.ActivateInteractionTask();
    }

    public void DeactivateInteractionTask()
    {
        _model.DeactivateInteractionTask();
    }

    #endregion
}

public interface ITaskVisualProvider
{
    public void SetRandomTasks();


    public void ActivateCells();
    public void DeactivateCells();


    public void ActivateInteractionTask();
    public void DeactivateInteractionTask();
}

public interface ITaskVisualEventsProvider
{
    public event Action OnChooseCell;

    public event Action<(TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition, int TaskId)> OnChooseTask_Value;
    public event Action OnChooseTask;
}
