using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskVisual : MonoBehaviour
{
    public TaskType Type => _taskType;

    [SerializeField] private List<Image> imagesStown = new();
    [SerializeField] private TextMeshProUGUI textTask;
    [SerializeField] private List<TaskVisualCell> cells = new();

    private TaskType _taskType;

    public void Initialize()
    {
        cells.ForEach(data => data.OnChoose += ChooseCell);
    }

    public void Dispose()
    {

    }

    public void SetData(Sprite spriteStown, string task, TaskType taskType)
    {
        _taskType = taskType;

        imagesStown.ForEach(data => data.sprite = spriteStown);

        textTask.text = task;
    }

    public void SetNumberValue(int cell, NumberValue numberValue, Color colorText)
    {
        cells.FirstOrDefault(data => data.Id == cell).SetData(numberValue, colorText);
    }

    public void ActivateCells()
    {
        cells.ForEach(data => data.Activate());
    }

    public void DeactivateCells()
    {
        cells.ForEach(data => data.Activate());
    }

    #region Output

    public event Action<TaskType, int> OnChooseCell;

    private void ChooseCell(int index)
    {
        OnChooseCell?.Invoke(_taskType, index);
    }

    #endregion
}
