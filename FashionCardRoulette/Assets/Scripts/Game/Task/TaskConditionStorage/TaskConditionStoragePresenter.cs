using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskConditionStoragePresenter : ITaskConditionStorageProvider
{
    private readonly TaskConditionStorageModel _model;

    public TaskConditionStoragePresenter(TaskConditionStorageModel model)
    {
        _model = model;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public ITaskCondition GetTaskConditionByTaskType(TaskType taskType)
    {
        return _model.GetTaskConditionByTaskType(taskType);
    }
}

public interface ITaskConditionStorageProvider
{
    public ITaskCondition GetTaskConditionByTaskType(TaskType taskType);
}
