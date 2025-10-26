using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlackAndOdd_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;


    private readonly int _requiredCount;


    public BlackAndOdd_TaskCondition(TaskType taskType, int id, int requiredCount, int claimCoins)
    {
        TaskType = taskType;
        ID = id;

        _requiredCount = requiredCount;

        TaskSmallDescription = $"black and odd number";
        TaskFullDescription = $"need to get a black odd number";
        ClaimCoins = claimCoins;
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        var matchingCells = usedCells.Where(kv => kv.Value.Color == ColorNumber.Black && kv.Value.Number % 2 != 0);

        if (matchingCells.Count() >= _requiredCount)
        {
            OnTaskConditionMet_CellIndexes?.Invoke(matchingCells.Select(kv => kv.Key).Take(_requiredCount).ToList());
            return true;
        }

        return false;
    }
}
