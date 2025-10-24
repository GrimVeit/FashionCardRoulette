using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SumAtLeast_TaskCondition : ITaskCondition
{
    public string TaskName { get; }
    public TaskType TaskType { get; }
    public int ID { get; }

    private readonly int _targetSum;

    public event Action<List<NumberValue>> OnTaskConditionMet;

    public SumAtLeast_TaskCondition(TaskType taskType, int id, int targetSum)
    {
        TaskType = taskType;
        ID = id;

        _targetSum = targetSum;

        TaskName = $"sum >= {_targetSum}";
    }

    public bool IsMet(List<NumberValue> numberValues)
    {
        int sum = numberValues.Sum(n => n.Number);

        if(sum >= _targetSum)
        {
            OnTaskConditionMet?.Invoke(numberValues);
            return true;
        }

        return false;
    }
}
