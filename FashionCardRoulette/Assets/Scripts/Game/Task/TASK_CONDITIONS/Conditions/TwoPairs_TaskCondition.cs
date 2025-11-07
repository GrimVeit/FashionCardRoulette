using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TwoPairs_TaskCondition : ITaskCondition
{
    public int ClaimCoins { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;

    private readonly int _firstPairNumber;
    private readonly int _secondPairNumber;

    public TwoPairs_TaskCondition(TaskType taskType, int id, int firstPairNumber, int secondPairNumber, int claimCoins)
    {
        TaskType = taskType;
        ID = id;

        _firstPairNumber = firstPairNumber;
        _secondPairNumber = secondPairNumber;

        TaskSmallDescription = $"two pairs: {_firstPairNumber} & {_secondPairNumber}";
        TaskFullDescription = $"need to get two pairs of numbers: {_firstPairNumber}-{_firstPairNumber} and {_secondPairNumber}-{_secondPairNumber}";
        ClaimCoins = claimCoins;
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        // сортируем по ключу (индексу)
        var ordered = usedCells.OrderBy(x => x.Key).ToArray();
        List<int> indexesFound = new();

        // словарь для подсчета встречаемости числа
        var numberCounts = new Dictionary<int, List<int>>();

        for (int i = 0; i < ordered.Length; i++)
        {
            int number = ordered[i].Value.Number;
            int index = ordered[i].Key;

            if (!numberCounts.ContainsKey(number))
                numberCounts[number] = new List<int>();

            numberCounts[number].Add(index);
        }

        // проверяем первую пару
        if (!numberCounts.ContainsKey(_firstPairNumber) || numberCounts[_firstPairNumber].Count < 2)
            return false;

        // проверяем вторую пару
        if (!numberCounts.ContainsKey(_secondPairNumber) || numberCounts[_secondPairNumber].Count < 2)
            return false;

        // собираем индексы для события
        indexesFound.AddRange(numberCounts[_firstPairNumber].Take(2));
        indexesFound.AddRange(numberCounts[_secondPairNumber].Take(2));

        OnTaskConditionMet_CellIndexes?.Invoke(indexesFound);

        return true;
    }
}

