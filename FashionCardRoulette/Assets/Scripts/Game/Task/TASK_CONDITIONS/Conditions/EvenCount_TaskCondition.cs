using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvenCount_TaskCondition : ITaskCondition
{
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;


    private readonly int _requiredCount;

    public EvenCount_TaskCondition(TaskType taskType, int id, int requiredCount)
    {
        TaskType = taskType;
        ID = id;

        _requiredCount = requiredCount;

        TaskSmallDescription = $"{_requiredCount} even numbers";
        TaskFullDescription = $"need to get {_requiredCount} even numbers";
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        var evenCells = usedCells.Where(kv => kv.Value.Number % 2 == 0);

        if(evenCells.Count() >= _requiredCount)
        {
            OnTaskConditionMet_CellIndexes?.Invoke(evenCells.Select(kv => kv.Key).Take(_requiredCount).ToList());
            return true;
        }

        return false;
    }
}
