using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITaskCondition
{
    public string TaskName { get; }
    public TaskType TaskType { get; }
    public int ID { get; }

    bool IsMet(List<NumberValue> numberValues);

    public event Action<List<NumberValue>> OnTaskConditionMet;
}
