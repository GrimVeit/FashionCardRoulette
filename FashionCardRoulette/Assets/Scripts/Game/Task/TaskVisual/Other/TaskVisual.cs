using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskVisual : MonoBehaviour
{
    public TaskType Type => _taskCondition.TaskType;
    public int Id => _id;

    [SerializeField] private List<Image> imagesStown = new();
    [SerializeField] private TextMeshProUGUI textTask;
    [SerializeField] private List<TaskVisualCell> cells = new();

    private ITaskCondition _taskCondition = null;
    private int _id;

    public void Initialize(int id)
    {
        _id = id;

        cells.ForEach(data => data.OnChoose += ChooseCell);
    }

    public void Dispose()
    {

    }

    public void SetData(Sprite spriteStown, ITaskCondition taskCondition)
    {
        _taskCondition = taskCondition;

        imagesStown.ForEach(data => data.sprite = spriteStown);

        textTask.text = _taskCondition.TaskName;
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
        cells.ForEach(data => data.Deactivate());
    }

    #region Output

    public event Action<int, int> OnChooseCell;

    private void ChooseCell(int index)
    {
        OnChooseCell?.Invoke(_id, index);
    }

    #endregion
}
