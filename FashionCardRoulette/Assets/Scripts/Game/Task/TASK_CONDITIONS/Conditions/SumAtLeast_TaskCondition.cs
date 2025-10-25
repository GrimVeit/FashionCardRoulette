using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SumAtLeast_TaskCondition : ITaskCondition
{
    public string TaskName { get; }
    public TaskType TaskType { get; }
    public int ID { get; }
    public int NeedCountNumber { get; } = 5;
    public event Action<List<int>> OnTaskConditionMet_CellIndexes;


    private readonly int _targetSum;

    public SumAtLeast_TaskCondition(TaskType taskType, int id, int targetSum)
    {
        TaskType = taskType;
        ID = id;

        _targetSum = targetSum;

        TaskName = $"sum >= {_targetSum}";
    }

    public bool IsMet(Dictionary<int, NumberValue> usedCells)
    {
        int sum = usedCells.Values.Sum(nv => nv.Number);

        if(sum >= _targetSum)
        {
            OnTaskConditionMet_CellIndexes?.Invoke(usedCells.Keys.ToList());
            return true;
        }

        return false;
    }
}
