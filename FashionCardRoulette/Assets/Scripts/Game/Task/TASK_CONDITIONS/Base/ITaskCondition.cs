using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITaskCondition
{
    public int ClaimCoins { get; }
    public int NeedCountNumber { get; }
    public string TaskSmallDescription { get; }
    public string TaskFullDescription { get; }
    public TaskType TaskType { get; }
    public int ID { get; }

    bool IsMet(Dictionary<int, NumberValue> usedCells);

    public event Action<List<int>> OnTaskConditionMet_CellIndexes;
}
