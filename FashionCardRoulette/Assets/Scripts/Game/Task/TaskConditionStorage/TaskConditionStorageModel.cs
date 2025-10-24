using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskConditionStorageModel
{
    private readonly Dictionary<TaskType, List<ITaskCondition>> conditions = new();

    public TaskConditionStorageModel()
    {
        conditions[TaskType.Easy] = new List<ITaskCondition>()
        {
            new SameColor_TaskCondition(TaskType.Easy, 0, requiredCount: 2),
            new SameNumber_TaskCondition(TaskType.Easy, 1, requiredCount: 2),
            new SumAtLeast_TaskCondition(TaskType.Easy, 2, targetSum: 10)

        };
    }
}
