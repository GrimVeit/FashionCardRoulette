using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SumAtLeast_TaskCondition : ITaskCondition
{
    public TaskType TaskType { get; }
    public int ID { get; }

    private readonly int _targetSum;

    public SumAtLeast_TaskCondition(TaskType taskType, int id, int targetSum)
    {
        TaskType = taskType;
        ID = id;

        _targetSum = targetSum;
    }

    public bool IsMet(List<NumberValue> numberValues)
    {
        return numberValues.Sum(n => n.Number) >= _targetSum;
    }
}
