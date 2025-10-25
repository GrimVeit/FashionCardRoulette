using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SameColor_TaskCondition : ITaskCondition
{
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;


    private readonly int _requiredCount;

    public SameColor_TaskCondition(TaskType taskType, int id, int requiredCount)
    {
        TaskType = taskType;
        ID = id;

        _requiredCount = requiredCount;

        TaskSmallDescription = $"{_requiredCount} numbers of the same color";
        TaskFullDescription = $"get {_requiredCount} numbers of the same color";
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        var groups = usedCells.GroupBy(n => n.Value.Color);

        foreach (var group in groups)
        {
            if (group.Count() >= _requiredCount)
            {
                List<int> indexes = group.Select(kv => kv.Key).Take(_requiredCount).ToList();
                OnTaskConditionMet_CellIndexes?.Invoke(indexes);
                return true;
            }
        }

        return false;
    }
}
