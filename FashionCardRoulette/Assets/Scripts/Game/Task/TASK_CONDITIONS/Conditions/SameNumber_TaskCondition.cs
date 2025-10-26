using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SameNumber_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;


    private readonly int _requiredCount;

    public SameNumber_TaskCondition(TaskType taskType, int id, int requiredCount, int claimCoins)
    {
        TaskType = taskType;
        ID = id;

        _requiredCount = requiredCount;

        TaskSmallDescription = $"{_requiredCount} numbers of the same value";
        TaskFullDescription = $"get {_requiredCount} numbers of the same value";
        ClaimCoins = claimCoins;
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        var groups = usedCells.GroupBy(n => n.Value.Number);

        foreach(var group in groups)
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
