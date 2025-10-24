using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            new SumAtLeast_TaskCondition(TaskType.Easy, 2, targetSum: 10),
            new EvenCount_TaskCondition(TaskType.Easy, 3, requiredCount: 2),
            new OddCount_TaskCondition(TaskType.Easy, 4, requiredCount: 2),
            new RedAndEven_TaskCondition(TaskType.Easy, 5, requiredCount: 1),
            new BlackAndOdd_TaskCondition(TaskType.Easy, 6, requiredCount: 1),
            new ConsecutivePair_TaskCondition(TaskType.Easy, 7, Random.Range(0, 36), Random.Range(0, 36))
        };

        conditions[TaskType.Middle] = new List<ITaskCondition>()
        {
            new SameColor_TaskCondition(TaskType.Middle, 0, requiredCount: 3),
            new SameNumber_TaskCondition(TaskType.Middle, 1, requiredCount: 3),
        };

        conditions[TaskType.Hard] = new List<ITaskCondition>()
        {
            new SameColor_TaskCondition(TaskType.Hard, 0, requiredCount: 4),
            new SameNumber_TaskCondition(TaskType.Hard, 1, requiredCount: 4),
        };

        conditions[TaskType.VeryHard
            ] = new List<ITaskCondition>()
        {
            new SameColor_TaskCondition(TaskType.Hard, 0, requiredCount: 5),
            new SameNumber_TaskCondition(TaskType.Hard, 1, requiredCount: 5),
        };
    }

    public ITaskCondition GetTaskConditionByTaskType(TaskType taskType)
    {
        if(!conditions.TryGetValue(taskType, out var list) || list.Count == 0) return null;

        int index = Random.Range(0, list.Count);
        return list[index];
    }
}
