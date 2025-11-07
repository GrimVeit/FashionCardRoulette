using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivisibleBy_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;

    private int _divNumber;

    public DivisibleBy_TaskCondition(TaskType taskType, int id, int divNumber, int claimCoins)
    {
        TaskType = taskType;
        ID = id;
        ClaimCoins = claimCoins;
        _divNumber = divNumber;

        TaskSmallDescription = $"numbers divisible by {_divNumber}";
        TaskFullDescription = $"need to get numbers where all numbers are divisible by {_divNumber}";
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        if(usedCells.Count != NeedCountNumber) return false;

        List<int> matchingIndexes = new();

        foreach (var kvp in usedCells)
        {
            if (kvp.Value.Number % _divNumber == 0)
            {
                matchingIndexes.Add(kvp.Key);
            }
            else
            {
                return false;
            }
        }

        if (matchingIndexes.Count > 0)
        {
            OnTaskConditionMet_CellIndexes?.Invoke(matchingIndexes);
            return true;
        }

        return false;
    }
}

