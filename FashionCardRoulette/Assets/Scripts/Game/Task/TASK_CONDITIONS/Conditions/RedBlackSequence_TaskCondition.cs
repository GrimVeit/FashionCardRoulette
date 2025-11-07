using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RedBlackSequence_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;

    public RedBlackSequence_TaskCondition(TaskType taskType, int id, int claimCoins)
    {
        TaskType = taskType;
        ID = id;
        ClaimCoins = claimCoins;
        TaskSmallDescription = "red-black-red-black-red sequence";
        TaskFullDescription = "numbers must follow the color sequence: red, black, red, black, red";
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        if (usedCells.Count < NeedCountNumber)
            return false;

        var ordered = usedCells.OrderBy(x => x.Key).ToArray();

        ColorNumber[] pattern = { ColorNumber.Red, ColorNumber.Black, ColorNumber.Red, ColorNumber.Black, ColorNumber.Red };

        for (int i = 0; i < NeedCountNumber; i++)
        {
            if (ordered[i].Value.Color != pattern[i])
                return false;
        }

        OnTaskConditionMet_CellIndexes?.Invoke(ordered.Select(x => x.Key).ToList());
        return true;
    }
}

