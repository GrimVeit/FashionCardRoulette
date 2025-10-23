using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TaskVisualView : View
{
    [SerializeField] private TaskVisual taskVisualPrefab;

    [SerializeField] private TaskPositions taskPositions;
    [SerializeField] private TaskDatas taskDatas;
    [SerializeField] private TaskSprites taskSprites;

    private List<TaskVisual> taskVisuals = new();
    private readonly List<TaskType> taskTypes = new() { TaskType.Easy, TaskType.Middle, TaskType.Hard, TaskType.VeryHard};

    public void ResetTasks()
    {
        if(taskVisuals.Count > 0)
        {
            for (int i = 0; i < taskVisuals.Count; i++)
            {
                Destroy(taskVisuals[i].gameObject);
            }

            taskVisuals.Clear();
        }

        for (int i = 0; i < taskTypes.Count; i++)
        {
            var parent = taskPositions.GetTransformParent(taskTypes[i]);

            var visual = Instantiate(taskVisualPrefab, parent.transform);
            visual.SetData(taskSprites.GetRandomTaskSprite(taskTypes[i]), taskDatas.GetRandomTaskData(taskTypes[i]).TextTask);
        }
    }
}

public enum TaskType
{
    Easy, Middle, Hard, VeryHard
}

#region TaskSprite

[System.Serializable]
public class TaskSprites
{
    [SerializeField] private List<TaskSpritesGroup> taskSpritesGroup = new();

    public Sprite GetRandomTaskSprite(TaskType taskType)
    {
        return taskSpritesGroup.FirstOrDefault(data => data.Type == taskType).GetRandomTaskSprite();
    }
}

[System.Serializable]
public class TaskSpritesGroup
{
    [SerializeField] private TaskType type;

    [SerializeField] private List<Sprite> sprites = new();

    public TaskType Type => type;

    public Sprite GetRandomTaskSprite()
    {
        return sprites[Random.Range(0, sprites.Count)];
    }
}

#endregion

#region TaskPosition

[System.Serializable]
public class TaskPositions
{
    [SerializeField] private List<TaskPosition> taskPositions = new();
    
    public Transform GetTransformParent(TaskType type)
    {
        return taskPositions.FirstOrDefault(data => data.Type == type).Parent;
    }
}

[System.Serializable]
public class TaskPosition
{
    [SerializeField] private TaskType taskType;
    [SerializeField] private Transform transformParent;

    public TaskType Type => taskType;
    public Transform Parent => transformParent;
}

#endregion

#region Task Data

[System.Serializable]
public class TaskDatas
{
    [SerializeField] private List<TaskDatasGroup> groups = new();

    public TaskData GetRandomTaskData(TaskType taskType)
    {
        return groups.FirstOrDefault(data => data.TaskType == taskType).GetRandomTaskData();
    }
}

[System.Serializable]
public class TaskDatasGroup
{
    [SerializeField] private TaskType taskType;

    [SerializeField] private List<TaskData> tasks;

    public TaskData GetRandomTaskData()
    {
        return tasks[Random.Range(0, tasks.Count)];
    }

    public TaskType TaskType => taskType;
}

[System.Serializable]
public class TaskData
{
    [SerializeField] private string textTask;

    public string TextTask => textTask;
}

#endregion
