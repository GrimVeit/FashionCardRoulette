using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskVisualModel
{
    private List<(TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition)> tasksAll = new() 
    { 
        (TaskType.Easy, TaskStatus.Active, null), 
        (TaskType.Hard, TaskStatus.Claimable, null), 
        (TaskType.Medium, TaskStatus.Completed, null), 
        (TaskType.VeryHard, TaskStatus.Failed, null) 
    };

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

    public void ChooseTask(int taskId)
    {
        var task = tasksAll[taskId];

        OnChooseTask_Value?.Invoke(task);

        OnChooseTask?.Invoke();
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




    public event Action OnChooseTask;
    public event Action<(TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition)> OnChooseTask_Value;

    #endregion

    #region Input

    public void SetRandomTasks()
    {
        List<ITaskCondition> tasks = new();

        for (int i = 0; i < tasksAll.Count; i++)
        {
            var taskCondition = _taskConditionStorageProvider.GetTaskConditionByTaskType(tasksAll[i].TaskType);

            tasksAll[i] = (tasksAll[i].TaskType, tasksAll[i].Status, taskCondition);

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
