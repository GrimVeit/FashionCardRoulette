using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskVisualModel
{
    private readonly List<TaskType> taskTypePattern = new() { TaskType.Easy, TaskType.Easy, TaskType.Middle, TaskType.Easy };

    private readonly ITaskConditionStorageProvider _taskConditionStorageProvider;
    private readonly IChooseNumberEventsProvider _chooseNumberEventsProvider;

    private NumberValue _currentNumberValue;

    public TaskVisualModel(ITaskConditionStorageProvider taskConditionStorageProvider, IChooseNumberEventsProvider chooseNumberEventsProvider)
    {
        _taskConditionStorageProvider = taskConditionStorageProvider;
        _chooseNumberEventsProvider = chooseNumberEventsProvider;
    }

    public void Initialize()
    {
        _chooseNumberEventsProvider.OnSetNumber_Value += SetNumberValue;
    }

    public void Dispose()
    {
        _chooseNumberEventsProvider.OnSetNumber_Value -= SetNumberValue;
    }

    public void ChooseCell(int taskId, int cellId)
    {
        if(_currentNumberValue == null)
        {
            Debug.LogError($"Not found NumberValue");
            return;
        }

        OnChooseCell_Value?.Invoke(taskId, cellId, _currentNumberValue);

        OnChooseCell?.Invoke();
    }

    private void SetNumberValue(NumberValue numberValue)
    {
        _currentNumberValue = numberValue;
    }

    #region Output

    public event Action<List<ITaskCondition>> OnSetTaskConditions;

    public event Action OnActivateCells;
    public event Action OnDeactivateCells;

    public event Action<int, int, NumberValue> OnChooseCell_Value;
    public event Action OnChooseCell;

    #endregion

    #region Input

    public void SetRandomTasks()
    {
        List<ITaskCondition> tasks = new();

        for (int i = 0; i < taskTypePattern.Count; i++)
        {
            var taskCondition = _taskConditionStorageProvider.GetTaskConditionByTaskType(taskTypePattern[i]);

            tasks.Add(taskCondition);
        }

        OnSetTaskConditions?.Invoke(tasks);
    }

    public void ActivateCells()
    {
        OnActivateCells?.Invoke();
    }

    public void DeactivateCells()
    {
        OnDeactivateCells?.Invoke();
    }

    #endregion
}
