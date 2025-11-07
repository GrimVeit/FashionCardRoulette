using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvenOddSequence_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;

    public EvenOddSequence_TaskCondition(TaskType taskType, int id, int claimCoins)
    {
        TaskType = taskType;
        ID = id;
        ClaimCoins = claimCoins;
        TaskSmallDescription = "even-odd-even-odd-even sequence";
        TaskFullDescription = "numbers must follow the sequence: even, odd, even, odd, even";
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        if (usedCells.Count < NeedCountNumber)
            return false;

        var ordered = usedCells.OrderBy(x => x.Key).ToArray();
        int[] pattern = { 0, 1, 0, 1, 0 }; // 0 - even, 1 - odd

        for (int i = 0; i < NeedCountNumber; i++)
        {
            int number = ordered[i].Value.Number;
            if ((number % 2) != pattern[i])
                return false;
        }

        OnTaskConditionMet_CellIndexes?.Invoke(ordered.Select(x => x.Key).ToList());
        return true;
    }
}

