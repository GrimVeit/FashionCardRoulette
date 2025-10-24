using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TaskVisualView : View
{
    [SerializeField] private TaskVisual taskVisualPrefab;

    [SerializeField] private TaskPositions taskPositions;
    [SerializeField] private TaskSprites taskSprites;
    [SerializeField] private TaskNumberColors taskNumberColors;

    private readonly List<TaskVisual> taskVisuals = new();

    public void SetTasks(List<ITaskCondition> taskConditions)
    {
        if (taskVisuals.Count > 0)
        {
            for (int i = 0; i < taskVisuals.Count; i++)
            {
                taskVisuals[i].OnChooseCell -= ChooseCell;
                taskVisuals[i].Dispose();

                Destroy(taskVisuals[i].gameObject);
            }

            taskVisuals.Clear();
        }

        for (int i = 0; i < taskConditions.Count; i++)
        {
            var parent = taskPositions.GetTransformParent(i);

            var visual = Instantiate(taskVisualPrefab, parent.transform);
            visual.SetData(taskSprites.GetRandomTaskSprite(taskConditions[i].TaskType), taskConditions[i]);

            visual.OnChooseCell += ChooseCell;
            visual.Initialize(i);

            taskVisuals.Add(visual);
        }
    }

    public void ActivateCells()
    {
        taskVisuals.ForEach(data => data.ActivateCells());
    }

    public void DeactivateCells()
    {
        taskVisuals.ForEach(data => data.DeactivateCells());
    }

    public void SetNumberValue(int taskId, int cellId, NumberValue numberValue)
    {
        var visual = taskVisuals.FirstOrDefault(data => data.Id == taskId);

        if(visual == null)
        {
            Debug.LogError("Not found TaskVisual with id - " + taskId);
            return;
        }

        visual.SetNumberValue(cellId, numberValue, taskNumberColors.GetTextColorNumber(numberValue.Color));
    }

    #region Output

    public event Action<int, int> OnChooseCell;

    private void ChooseCell(int taskId, int cellId)
    {
        OnChooseCell?.Invoke(taskId, cellId);
    }

    #endregion
}

public enum TaskType
{
    Easy, Middle, Hard, VeryHard
}

#region Task Sprite

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

#region Task Position

[System.Serializable]
public class TaskPositions
{
    [SerializeField] private List<TaskPosition> taskPositions = new();
    
    public Transform GetTransformParent(int id)
    {
        return taskPositions.FirstOrDefault(data => data.ID == id).Parent;
    }
}

[System.Serializable]
public class TaskPosition
{
    [SerializeField] private int id;
    [SerializeField] private Transform transformParent;

    public int ID => id;
    public Transform Parent => transformParent;
}

#endregion

#region Task Number Color

[System.Serializable]
public class TaskNumberColors
{
    [SerializeField] private List<TaskNumberColor> colors = new();

    public Color GetTextColorNumber(ColorNumber colorNumber)
    {
        return colors.FirstOrDefault(data => data.ColorNumber == colorNumber).ColorText;
    }

}

[System.Serializable]
public class TaskNumberColor
{
    [SerializeField] private ColorNumber colorNumber;
    [SerializeField] private Color colorText;

    public ColorNumber ColorNumber => colorNumber;
    public Color ColorText => colorText;
}

#endregion
