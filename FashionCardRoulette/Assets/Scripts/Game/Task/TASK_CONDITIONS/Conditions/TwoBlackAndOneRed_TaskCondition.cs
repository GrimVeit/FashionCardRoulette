using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TwoBlackOneRed_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;

    public TwoBlackOneRed_TaskCondition(TaskType taskType, int id, int claimCoins)
    {
        TaskType = taskType;
        ID = id;
        ClaimCoins = claimCoins;

        TaskSmallDescription = "two black and one red";
        TaskFullDescription = "need to get at least two black numbers and one red number on the card";
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        var blackCells = usedCells.Where(c => c.Value.Color == ColorNumber.Black).ToList();
        var redCells = usedCells.Where(c => c.Value.Color == ColorNumber.Red).ToList();

        if (blackCells.Count >= 2 && redCells.Count >= 1)
        {
            var indexes = blackCells.Take(2).Select(c => c.Key)
                .Concat(redCells.Take(1).Select(c => c.Key))
                .ToList();

            OnTaskConditionMet_CellIndexes?.Invoke(indexes);
            return true;
        }

        return false;
    }
}

