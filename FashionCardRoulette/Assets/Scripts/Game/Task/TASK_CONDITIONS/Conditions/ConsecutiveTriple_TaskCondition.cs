using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConsecutiveTriple_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;

    private readonly int _firstNumber;
    private readonly int _secondNumber;
    private readonly int _thirdNumber;

    public ConsecutiveTriple_TaskCondition(TaskType taskType, int id, int firstNumber, int secondNumber, int thirdNumber, int claimCoins)
    {
        TaskType = taskType;
        ID = id;

        _firstNumber = firstNumber;
        _secondNumber = secondNumber;
        _thirdNumber = thirdNumber;

        TaskSmallDescription = $"consecutive numbers: {_firstNumber}, {_secondNumber}, {_thirdNumber}";
        TaskFullDescription = $"need to get three consecutive numbers: {_firstNumber}, {_secondNumber}, {_thirdNumber}";
        ClaimCoins = claimCoins;
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        if (usedCells.Count < 3)
            return false;

        var ordered = usedCells.OrderBy(x => x.Key).ToArray();

        for (int i = 0; i < ordered.Length - 2; i++)
        {
            int index1 = ordered[i].Key;
            int index2 = ordered[i + 1].Key;
            int index3 = ordered[i + 2].Key;

            if (index2 == index1 + 1 && index3 == index2 + 1)
            {
                int num1 = ordered[i].Value.Number;
                int num2 = ordered[i + 1].Value.Number;
                int num3 = ordered[i + 2].Value.Number;

                if (num1 == _firstNumber && num2 == _secondNumber && num3 == _thirdNumber)
                {
                    OnTaskConditionMet_CellIndexes?.Invoke(new() { index1, index2, index3 });
                    return true;
                }
            }
        }

        return false;
    }
}

