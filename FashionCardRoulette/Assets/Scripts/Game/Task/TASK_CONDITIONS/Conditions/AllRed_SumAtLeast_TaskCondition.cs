using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllRed_SumAtLeast_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;

    private readonly int _minSum;

    public AllRed_SumAtLeast_TaskCondition(TaskType taskType, int id, int minSum, int claimCoins)
    {
        TaskType = taskType;
        ID = id;
        _minSum = minSum;

        TaskSmallDescription = $"all red, sum ≥ {_minSum}";
        TaskFullDescription = $"all numbers must be red and sum at least {_minSum}";
        ClaimCoins = claimCoins;
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        if(usedCells.Count != NeedCountNumber) return false;

        var indexesFound = new List<int>();
        int sum = 0;

        foreach (var kvp in usedCells)
        {
            if (kvp.Value.Color != ColorNumber.Red)
                return false;

            indexesFound.Add(kvp.Key);
            sum += kvp.Value.Number;
        }

        if (sum >= _minSum)
        {
            OnTaskConditionMet_CellIndexes?.Invoke(indexesFound);
            return true;
        }

        return false;
    }
}

