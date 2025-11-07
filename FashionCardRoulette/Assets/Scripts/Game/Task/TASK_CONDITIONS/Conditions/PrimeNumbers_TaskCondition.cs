using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrimeNumbers_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;

    public PrimeNumbers_TaskCondition(TaskType taskType, int id, int claimCoins)
    {
        TaskType = taskType;
        ID = id;
        ClaimCoins = claimCoins;
        TaskSmallDescription = "all numbers are prime";
        TaskFullDescription = "all selected numbers must be prime numbers (2, 3, 5, 7, 11, 13...)";
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        if (usedCells.Count < NeedCountNumber)
            return false;

        foreach (var cell in usedCells)
        {
            if (!IsPrime(cell.Value.Number))
                return false;
        }

        OnTaskConditionMet_CellIndexes?.Invoke(usedCells.Keys.ToList());
        return true;
    }

    private bool IsPrime(int number)
    {
        if (number < 2) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        for (int i = 3; i * i <= number; i += 2)
        {
            if (number % i == 0)
                return false;
        }

        return true;
    }
}

