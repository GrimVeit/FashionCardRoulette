using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConsecutiveQuad_TaskCondition : ITaskCondition
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
    private readonly int _numberThird;
    private readonly int _numberFourth;

    public ConsecutiveQuad_TaskCondition(TaskType taskType, int id, int numberFirst, int numberSecond, int numberThird, int numberFourth, int claimCoins)
    {
        TaskType = taskType;
        ID = id;

        _numberFirst = numberFirst;
        _numberSecond = numberSecond;
        _numberThird = numberThird;
        _numberFourth = numberFourth;

        TaskSmallDescription = $"consecutive numbers: {_numberFirst}, {_numberSecond}, {_numberThird}, {_numberFourth}";
        TaskFullDescription = $"need to get four consecutive numbers: {_numberFirst}, {_numberSecond}, {_numberThird}, {_numberFourth}";
        ClaimCoins = claimCoins;
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        var ordered = usedCells.OrderBy(x => x.Key).ToArray();

        for (int i = 0; i < ordered.Length - 3; i++)
        {
            int index1 = ordered[i].Key;
            int index2 = ordered[i + 1].Key;
            int index3 = ordered[i + 2].Key;
            int index4 = ordered[i + 3].Key;

            if (index2 == index1 + 1 && index3 == index2 + 1 && index4 == index3 + 1)
            {
                int num1 = ordered[i].Value.Number;
                int num2 = ordered[i + 1].Value.Number;
                int num3 = ordered[i + 2].Value.Number;
                int num4 = ordered[i + 3].Value.Number;

                if (num1 == _numberFirst && num2 == _numberSecond && num3 == _numberThird && num4 == _numberFourth)
                {
                    OnTaskConditionMet_CellIndexes?.Invoke(new List<int> { index1, index2, index3, index4 });
                    return true;
                }
            }
        }

        return false;
    }
}

