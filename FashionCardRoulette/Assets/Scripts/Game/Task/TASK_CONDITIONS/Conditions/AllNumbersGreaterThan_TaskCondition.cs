using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllNumbersGreaterThan_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;

    private readonly int _minValue;

    public AllNumbersGreaterThan_TaskCondition(TaskType taskType, int id, int minValue, int claimCoins)
    {
        TaskType = taskType;
        ID = id;
        _minValue = minValue;

        TaskSmallDescription = $"all numbers > {_minValue}";
        TaskFullDescription = $"all numbers must be greater than {_minValue}";
        ClaimCoins = claimCoins;
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        if (usedCells.Count != NeedCountNumber) return false;

        var indexesFound = new List<int>();

        foreach (var kvp in usedCells)
        {
            if (kvp.Value.Number <= _minValue)
                return false;

            indexesFound.Add(kvp.Key);
        }

        OnTaskConditionMet_CellIndexes?.Invoke(indexesFound);
        return true;
    }
}

