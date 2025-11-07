using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllSameColorAndOdd_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;

    public AllSameColorAndOdd_TaskCondition(TaskType taskType, int id, int claimCoins)
    {
        TaskType = taskType;
        ID = id;
        ClaimCoins = claimCoins;
        TaskSmallDescription = "all numbers same color and odd";
        TaskFullDescription = "all numbers must be odd and all the same color (all red or all black)";
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        if (usedCells.Count < NeedCountNumber)
            return false;

        var firstColor = usedCells.First().Value.Color;

        foreach (var cell in usedCells.Values)
        {
            if (cell.Number % 2 == 0) // проверяем на нечётность
                return false;
            if (cell.Color != firstColor)
                return false;
        }

        OnTaskConditionMet_CellIndexes?.Invoke(usedCells.Keys.ToList());
        return true;
    }
}


