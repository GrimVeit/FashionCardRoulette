using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskVisualModel
{
    private readonly List<TaskType> taskTypesList = new() { TaskType.Easy, TaskType.Middle, TaskType.Hard, TaskType.VeryHard };

    public void ResetTasks()
    {
        OnResetTasks?.Invoke();
    }

    #region Output

    public event Action OnResetTasks;

    #endregion
}
