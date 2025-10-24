using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvenCount_TaskCondition : ITaskCondition
{
    public TaskType TaskType { get; }
    public int ID { get; }

    private readonly int _requiredCount;

    public EvenCount_TaskCondition(TaskType taskType, int id, int requiredCount)
    {
        TaskType = taskType;
        ID = id;

        _requiredCount = requiredCount;
    }

    public bool IsMet(List<NumberValue> numberValues)
    {
        return numberValues.Sum(n => n.Number) >= _requiredCount;
    }
}
