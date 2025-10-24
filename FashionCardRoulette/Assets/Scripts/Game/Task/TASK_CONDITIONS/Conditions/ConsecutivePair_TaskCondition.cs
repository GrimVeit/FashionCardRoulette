using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConsecutivePair_TaskCondition : ITaskCondition
{
    public string TaskName { get; }
    public TaskType TaskType { get; }
    public int ID { get; }

    private readonly int _numberFirst;
    private readonly int _numberSecond;

    public event Action<List<NumberValue>> OnTaskConditionMet;

    public ConsecutivePair_TaskCondition(TaskType taskType, int id, int numberFirst, int numberSecond)
    {
        TaskType = taskType;
        ID = id;

        _numberFirst = numberFirst;
        _numberSecond = numberSecond;

        TaskName = $"Consecutive numbers: {_numberFirst}, {_numberSecond}";
    }

    public bool IsMet(List<NumberValue> numberValues)
    {
        for (int i = 0; i < numberValues.Count - 1; i++)
        {
            if (numberValues[i].Number == _numberFirst && numberValues[i + 1].Number == _numberSecond)
            {
                OnTaskConditionMet?.Invoke(new() { numberValues[i], numberValues[i + 1] });
                return true;
            }
        }

        return false;
    }
}
