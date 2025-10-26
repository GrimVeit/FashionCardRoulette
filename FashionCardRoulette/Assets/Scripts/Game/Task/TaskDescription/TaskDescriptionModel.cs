using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDescriptionModel
{
    private (TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition, int TaskId) _currentTask;

    private readonly ITaskVisualEventsProvider _taskVisualEventsProvider;
    private readonly ITaskVisualInfoProvider _taskVisualInfoProvider;
    private readonly ITaskVisualActivatorProvider _taskVisualActivatorProvider;
    private readonly IMoneyProvider _moneyProvider;  

    public TaskDescriptionModel(ITaskVisualEventsProvider taskVisualEventsProvider, ITaskVisualInfoProvider taskVisualInfoProvider, ITaskVisualActivatorProvider taskVisualActivatorProvider, IMoneyProvider moneyProvider)
    {
        _taskVisualEventsProvider = taskVisualEventsProvider;
        _taskVisualInfoProvider = taskVisualInfoProvider;
        _taskVisualActivatorProvider = taskVisualActivatorProvider;
        _moneyProvider = moneyProvider;

        _taskVisualEventsProvider.OnChooseTask_Value += SetTask;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _taskVisualEventsProvider.OnChooseTask_Value -= SetTask;
    }

    public void Claim(int taskId, int claimCoins)
    {
        if (_taskVisualInfoProvider.IsHaveTask(taskId))
        {
            _moneyProvider.SendMoney(claimCoins);

            _taskVisualActivatorProvider.CompleteTask(taskId);

            OnClaimTask?.Invoke();
        }
        else
        {
            Debug.LogError("Not found task with id - " + taskId);
        }
    }

    private void SetTask((TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition, int taskId) task)
    {
        _currentTask = task;

        OnSetTask?.Invoke(_currentTask);
    }

    #region Output

    public event Action<(TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition, int taskId)> OnSetTask;

    public event Action OnClaimTask;

    #endregion
}
