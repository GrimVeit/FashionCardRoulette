using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConsecutivePair_TaskCondition : ITaskCondition
{
    public string TaskName { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;


    private readonly int _numberFirst;
    private readonly int _numberSecond;

    public ConsecutivePair_TaskCondition(TaskType taskType, int id, int numberFirst, int numberSecond)
    {
        TaskType = taskType;
        ID = id;

        _numberFirst = numberFirst;
        _numberSecond = numberSecond;

        TaskName = $"Consecutive numbers: {_numberFirst}, {_numberSecond}";
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        var ordered = usedCells.OrderBy(x => x.Key).ToArray();

        for (int i = 0; i < ordered.Length - 1; i++)
        {
            int current = ordered[i].Value.Number;
            int next = ordered[i + 1].Value.Number;

            if(next == current + 1)
            {
                return true;
            }
        }

        return false;
    }
}
