using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OddCount_TaskCondition : ITaskCondition
{
    public string TaskName { get; }
    public TaskType TaskType { get; }
    public int ID { get; }

    private readonly int _requiredCount;

    public event Action<List<NumberValue>> OnTaskConditionMet;

    public OddCount_TaskCondition(TaskType taskType, int id, int requiredCount)
    {
        TaskType = taskType;
        ID = id;

        _requiredCount = requiredCount;

        TaskName = $"{_requiredCount} odd numbers";
    }

    public bool IsMet(List<NumberValue> numberValues)
    {
        var evenNumbers = numberValues.Where(n => n.Number % 2 != 0).ToArray();

        if (evenNumbers.Length >= _requiredCount)
        {
            OnTaskConditionMet?.Invoke(evenNumbers.Take(_requiredCount).ToList());
            return true;
        }

        return false;
    }
}
