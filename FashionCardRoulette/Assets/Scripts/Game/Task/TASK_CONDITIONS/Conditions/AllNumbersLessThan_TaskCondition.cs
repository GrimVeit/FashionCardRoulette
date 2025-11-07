using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllNumbersLessThan_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;

    private readonly int _maxValue;

    public AllNumbersLessThan_TaskCondition(TaskType taskType, int id, int maxValue, int claimCoins)
    {
        TaskType = taskType;
        ID = id;
        _maxValue = maxValue;

        TaskSmallDescription = $"all numbers < {_maxValue}";
        TaskFullDescription = $"all numbers must be less than {_maxValue}";
        ClaimCoins = claimCoins;
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        if(usedCells.Count != NeedCountNumber) return false;

        var indexesFound = new List<int>();

        foreach (var kvp in usedCells)
        {
            if (kvp.Value.Number >= _maxValue)
                return false;

            indexesFound.Add(kvp.Key);
        }

        OnTaskConditionMet_CellIndexes?.Invoke(indexesFound);
        return true;
    }
}

