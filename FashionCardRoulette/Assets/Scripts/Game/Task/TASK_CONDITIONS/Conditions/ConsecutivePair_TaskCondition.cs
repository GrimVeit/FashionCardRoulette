using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConsecutivePair_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;


    private readonly int _numberFirst;
    private readonly int _numberSecond;

    public ConsecutivePair_TaskCondition(TaskType taskType, int id, int numberFirst, int numberSecond, int claimCoins)
    {
        TaskType = taskType;
        ID = id;

        _numberFirst = numberFirst;
        _numberSecond = numberSecond;

        TaskSmallDescription = $"consecutive numbers: {_numberFirst}, {_numberSecond}";
        TaskFullDescription = $"need to get two consecutive numbers: {_numberFirst}, {_numberSecond}";
        ClaimCoins = claimCoins;
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        var ordered = usedCells.OrderBy(x => x.Key).ToArray();

        for (int i = 0; i < ordered.Length - 1; i++)
        {
            int index1 = ordered[i].Key;
            int index2 = ordered[i + 1].Key;

            if(index2 == index1 + 1)
            {
                int num1 = ordered[i].Value.Number;
                int num2 = ordered[i + 1].Value.Number;

                if(num1 == _numberFirst && num2 == _numberSecond)
                {
                    OnTaskConditionMet_CellIndexes?.Invoke(new() { index1,  index2 });
                }
            }
        }

        return false;
    }
}
