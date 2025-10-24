using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SameNumber_TaskCondition : ITaskCondition
{
    public TaskType TaskType { get; }
    public int ID { get; }

    private readonly int _requiredCount;

    public event Action<List<NumberValue>> OnTaskConditionMet;

    public SameNumber_TaskCondition(TaskType taskType, int id, int requiredCount)
    {
        TaskType = taskType;
        ID = id;

        _requiredCount = requiredCount;
    }

    public bool IsMet(List<NumberValue> numberValues)
    {
        var group = numberValues.GroupBy(n => n.Number).FirstOrDefault(g => g.Count() >= _requiredCount);

        if(group != null)
        {
            OnTaskConditionMet?.Invoke(group.Take(_requiredCount).ToList());
            return true;
        }

        return false;
    }
}
