using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDescriptionModel
{
    private readonly ITaskVisualEventsProvider _taskVisualEventsProvider;

    private (TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition) _currentTask;

    public TaskDescriptionModel(ITaskVisualEventsProvider taskVisualEventsProvider)
    {
        _taskVisualEventsProvider = taskVisualEventsProvider;

        _taskVisualEventsProvider.OnChooseTask_Value += SetTask;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _taskVisualEventsProvider.OnChooseTask_Value -= SetTask;
    }

    private void SetTask((TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition) task)
    {
        _currentTask = task;

        OnSetTask?.Invoke(_currentTask);
    }

    #region Output

    public event Action<(TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition)> OnSetTask;

    #endregion
}
