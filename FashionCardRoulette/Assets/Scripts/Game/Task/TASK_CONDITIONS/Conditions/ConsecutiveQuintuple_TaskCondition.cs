using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConsecutiveQuintuple_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;

    private readonly int _number1;
    private readonly int _number2;
    private readonly int _number3;
    private readonly int _number4;
    private readonly int _number5;

    public ConsecutiveQuintuple_TaskCondition(TaskType taskType, int id, int number1, int number2, int number3, int number4, int number5, int claimCoins)
    {
        TaskType = taskType;
        ID = id;

        _number1 = number1;
        _number2 = number2;
        _number3 = number3;
        _number4 = number4;
        _number5 = number5;

        TaskSmallDescription = $"consecutive numbers: {_number1}, {_number2}, {_number3}, {_number4}, {_number5}";
        TaskFullDescription = $"need to get five consecutive numbers: {_number1}, {_number2}, {_number3}, {_number4}, {_number5}";
        ClaimCoins = claimCoins;
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        var ordered = usedCells.OrderBy(x => x.Key).ToArray();

        for (int i = 0; i < ordered.Length - 4; i++)
        {
            int index1 = ordered[i].Key;
            int index2 = ordered[i + 1].Key;
            int index3 = ordered[i + 2].Key;
            int index4 = ordered[i + 3].Key;
            int index5 = ordered[i + 4].Key;

            if (index2 == index1 + 1 &&
                index3 == index2 + 1 &&
                index4 == index3 + 1 &&
                index5 == index4 + 1)
            {
                int num1 = ordered[i].Value.Number;
                int num2 = ordered[i + 1].Value.Number;
                int num3 = ordered[i + 2].Value.Number;
                int num4 = ordered[i + 3].Value.Number;
                int num5 = ordered[i + 4].Value.Number;

                if (num1 == _number1 && num2 == _number2 && num3 == _number3 && num4 == _number4 && num5 == _number5)
                {
                    OnTaskConditionMet_CellIndexes?.Invoke(new() { index1, index2, index3, index4, index5 });
                    return true;
                }
            }
        }

        return false;
    }
}

