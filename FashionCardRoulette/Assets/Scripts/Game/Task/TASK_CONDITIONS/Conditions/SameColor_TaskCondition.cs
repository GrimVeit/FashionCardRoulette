using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SameColor_TaskCondition : ITaskCondition
{
    public string TaskName { get; }
    public TaskType TaskType { get; }
    public int ID { get; }

    private readonly int _requiredCount;

    public event Action<List<NumberValue>> OnTaskConditionMet;

    public SameColor_TaskCondition(TaskType taskType, int id, int requiredCount)
    {
        TaskType = taskType;
        ID = id;

        _requiredCount = requiredCount;

        TaskName = $"{_requiredCount} numbers of the same color";
    }

    public bool IsMet(List<NumberValue> numberValues)
    {
        var group = numberValues.GroupBy(n => n.Color)
            .FirstOrDefault(g => g.Count() >= _requiredCount);

        if (group != null)
        {
            OnTaskConditionMet?.Invoke(group.Take(_requiredCount).ToList());
            return true;
        }

        return false;
    }
}
