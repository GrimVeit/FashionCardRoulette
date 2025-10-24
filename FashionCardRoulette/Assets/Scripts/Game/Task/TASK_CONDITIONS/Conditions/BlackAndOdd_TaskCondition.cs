using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlackAndOdd_TaskCondition : ITaskCondition
{
    public string TaskName { get; }
    public TaskType TaskType { get; }
    public int ID { get; }

    private readonly int _requiredCount;

    public event Action<List<NumberValue>> OnTaskConditionMet;

    public BlackAndOdd_TaskCondition(TaskType taskType, int id, int requiredCount)
    {
        TaskType = taskType;
        ID = id;

        _requiredCount = requiredCount;

        TaskName = $"black and odd number";
    }

    public bool IsMet(List<NumberValue> numberValues)
    {
        var matchedNumbers = numberValues.Where(n => n.Color == ColorNumber.Black && n.Number % 2 != 0).ToList();

        if (matchedNumbers.Count >= _requiredCount)
        {
            OnTaskConditionMet?.Invoke(matchedNumbers.Take(_requiredCount).ToList());
            return true;
        }

        return false;
    }
}
