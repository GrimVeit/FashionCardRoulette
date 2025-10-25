using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RedAndEven_TaskCondition : ITaskCondition
{
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;


    private readonly int _requiredCount;


    public RedAndEven_TaskCondition(TaskType taskType, int id, int requiredCount)
    {
        TaskType = taskType;
        ID = id;

        _requiredCount = requiredCount;

        TaskSmallDescription = $"red and even number";
        TaskFullDescription = $"need to get a red even number";
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        var matchingCells = usedCells.Where(kv => kv.Value.Color == ColorNumber.Red && kv.Value.Number % 2 == 0);

        if(matchingCells.Count() >= _requiredCount)
        {
            OnTaskConditionMet_CellIndexes?.Invoke(matchingCells.Select(kv => kv.Key).Take(_requiredCount).ToList());
            return true;
        }

        return false;
    }
}
