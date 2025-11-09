using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskVisualModel
{
    private List<(TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition)> tasksAll = new() 
    { 
        (TaskType.Easy, TaskStatus.InProgress, null), 
        (TaskType.Medium, TaskStatus.InProgress, null), 
        (TaskType.Hard, TaskStatus.InProgress, null), 
        (TaskType.VeryHard, TaskStatus.InProgress, null) 
    };

    private readonly ITaskConditionStorageProvider _taskConditionStorageProvider;
    private readonly IChooseNumberEventsProvider _chooseNumberEventsProvider;

    private NumberValue _currentNumberValue;

    private readonly ISoundProvider _soundProvider;

    public TaskVisualModel(ITaskConditionStorageProvider taskConditionStorageProvider, IChooseNumberEventsProvider chooseNumberEventsProvider, ISoundProvider soundProvider)
    {
        _taskConditionStorageProvider = taskConditionStorageProvider;
        _chooseNumberEventsProvider = chooseNumberEventsProvider;
        _soundProvider = soundProvider;
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

        _soundProvider.PlayOneShot("Click");
    }

    public void ChooseTask(int taskId)
    {
        var task = (tasksAll[taskId].TaskType, tasksAll[taskId].Status, tasksAll[taskId].TaskCondition, taskId);

        OnChooseTask_Value?.Invoke(task);

        OnChooseTask?.Invoke();

        _soundProvider.PlayOneShot("ClickTask");
    }

    public void SetInProgressTask(int taskId)
    {
        tasksAll[taskId] = (tasksAll[taskId].TaskType, TaskStatus.InProgress, tasksAll[taskId].TaskCondition);

        var task = tasksAll[taskId];

        OnSetInProgressTask?.Invoke(taskId);
    }

    public void SetClaimableTask(int taskId)
    {
        tasksAll[taskId] = (tasksAll[taskId].TaskType, TaskStatus.Claimable, tasksAll[taskId].TaskCondition);

        var task = tasksAll[taskId];

        OnSetClaimableTask?.Invoke(taskId);

        _soundProvider.PlayOneShot("TaskGood");
    }

    public void SetFailedTask(int taskId)
    {
        tasksAll[taskId] = (tasksAll[taskId].TaskType, TaskStatus.Failed, tasksAll[taskId].TaskCondition);

        var task = tasksAll[taskId];

        OnSetFailedTask?.Invoke(taskId);

        _soundProvider.PlayOneShot("TaskBad");
    }

    public void SetCompletedTask(int taskId)
    {
        tasksAll[taskId] = (tasksAll[taskId].TaskType, TaskStatus.Completed, tasksAll[taskId].TaskCondition);

        var task = tasksAll[taskId];

        OnSetCompletedTask?.Invoke(taskId);

        _soundProvider.PlayOneShot("TaskComplete");
    }

    private void SetNumberValue(NumberValue numberValue)
    {
        _currentNumberValue = numberValue;
    }



    public bool IsHaveTask(int taskId)
    {
        return taskId >= 0 && taskId < tasksAll.Count;
    }

    public bool IsAllTaskFinished()
    {
        return tasksAll.All(task =>
            task.Status == TaskStatus.Completed || task.Status == TaskStatus.Failed
        );
    }

    public int AllCountCells()
    {
        int count = 0;

        for (int i = 0; i < tasksAll.Count; i++)
        {
            count += tasksAll[i].TaskCondition.NeedCountNumber;
        }

        return count;
    }

    #region Output

    public event Action<List<ITaskCondition>> OnSetTaskConditions;

    public event Action OnActivateCells;
    public event Action OnDeactivateCells;

    public event Action OnActivateInteractionTask;
    public event Action OnDeactivateInteractionTask;

    public event Action<int> OnSetInProgressTask;
    public event Action<int> OnSetClaimableTask;
    public event Action<int> OnSetCompletedTask;
    public event Action<int> OnSetFailedTask;

    public event Action<int, int, NumberValue> OnChooseCell_Value;
    public event Action OnChooseCell;




    public event Action OnChooseTask;
    public event Action<(TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition, int TaskId)> OnChooseTask_Value;

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

        for (int i = 0; i < tasksAll.Count; i++)
        {
            SetInProgressTask(i);
        }
    }



    public void ActivateCells()
    {
        OnActivateCells?.Invoke();
    }

    public void DeactivateCells()
    {
        OnDeactivateCells?.Invoke();
    }


    public void ActivateInteractionTask()
    {
        OnActivateInteractionTask?.Invoke();
    }

    public void DeactivateInteractionTask()
    {
        OnDeactivateInteractionTask?.Invoke();
    }

    #endregion
}
