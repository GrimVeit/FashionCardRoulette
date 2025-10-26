using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskConditionStorageModel
{
    private readonly Dictionary<TaskType, List<ITaskCondition>> conditions = new();

    private readonly Dictionary<TaskType, List<int>> claimsCoins = new()
    {
        { TaskType.Easy, new List<int>() { 10, 15, 20, 25 } },
        { TaskType.Medium, new List<int>() { 30, 35, 40, 45, 50, 55, 60, 65 } },
        { TaskType.Hard, new List<int>() { 80, 85, 90, 95, 100, 110, 120 } },
        { TaskType.VeryHard, new List<int>() { 80, 85, 90, 95, 100, 110, 120 } },
    };

    public TaskConditionStorageModel()
    {




        conditions[TaskType.Easy] = new List<ITaskCondition>()
        {
            new SameColor_TaskCondition(TaskType.Easy, 0, requiredCount: 2, GetRandomReward(TaskType.Easy)),
            new SameNumber_TaskCondition(TaskType.Easy, 1, requiredCount: 2, GetRandomReward(TaskType.Easy)),
            new SumAtLeast_TaskCondition(TaskType.Easy, 2, targetSum: 10, GetRandomReward(TaskType.Easy)),
            new EvenCount_TaskCondition(TaskType.Easy, 3, requiredCount: 2, GetRandomReward(TaskType.Easy)),
            new OddCount_TaskCondition(TaskType.Easy, 4, requiredCount: 2, GetRandomReward(TaskType.Easy)),
            new RedAndEven_TaskCondition(TaskType.Easy, 5, requiredCount: 1, GetRandomReward(TaskType.Easy)),
            new BlackAndOdd_TaskCondition(TaskType.Easy, 6, requiredCount: 1, GetRandomReward(TaskType.Easy)),
            new ConsecutivePair_TaskCondition(TaskType.Easy, 7, Random.Range(0, 36), Random.Range(0, 36), GetRandomReward(TaskType.Easy))
        };

        conditions[TaskType.Medium] = new List<ITaskCondition>()
        {
            new SameColor_TaskCondition(TaskType.Medium, 0, requiredCount: 3, GetRandomReward(TaskType.Medium)),
            new SameNumber_TaskCondition(TaskType.Medium, 1, requiredCount: 3, GetRandomReward(TaskType.Medium))
        };

        conditions[TaskType.Hard] = new List<ITaskCondition>()
        {
            new SameColor_TaskCondition(TaskType.Hard, 0, requiredCount: 4, GetRandomReward(TaskType.Hard)),
            new SameNumber_TaskCondition(TaskType.Hard, 1, requiredCount: 4, GetRandomReward(TaskType.Hard))
        };

        conditions[TaskType.VeryHard] = new List<ITaskCondition>()
        {
            new SameColor_TaskCondition(TaskType.Hard, 0, requiredCount: 5, GetRandomReward(TaskType.VeryHard)),
            new SameNumber_TaskCondition(TaskType.Hard, 1, requiredCount: 5, GetRandomReward(TaskType.VeryHard))
        };
    }

    public ITaskCondition GetTaskConditionByTaskType(TaskType taskType)
    {
        if(!conditions.TryGetValue(taskType, out var list) || list.Count == 0) return null;

        int index = Random.Range(0, list.Count);

        var condition = list[index];

        conditions[taskType].Remove(condition);

        return condition;
    }

    private int GetRandomReward(TaskType taskType)
    {
        if (!claimsCoins.TryGetValue(taskType, out var rewards) || rewards.Count == 0)
            return 0;

        return rewards[UnityEngine.Random.Range(0, rewards.Count)];
    }
}
