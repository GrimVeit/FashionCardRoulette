using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TwoRedOneBlack_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;

    public TwoRedOneBlack_TaskCondition(TaskType taskType, int id, int claimCoins)
    {
        TaskType = taskType;
        ID = id;
        ClaimCoins = claimCoins;

        TaskSmallDescription = "two red and one black";
        TaskFullDescription = "need to get at least two red numbers and one black number on the card";
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        var redCells = usedCells.Where(c => c.Value.Color == ColorNumber.Red).ToList();
        var blackCells = usedCells.Where(c => c.Value.Color == ColorNumber.Black).ToList();

        if (redCells.Count >= 2 && blackCells.Count >= 1)
        {
            var indexes = redCells.Take(2).Select(c => c.Key)
                .Concat(blackCells.Take(1).Select(c => c.Key))
                .ToList();

            OnTaskConditionMet_CellIndexes?.Invoke(indexes);
            return true;
        }

        return false;
    }
}

