using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskVisual : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TaskStatus TaskStatus => _status;
    public TaskType Type => _taskCondition.TaskType;
    public int Id => _id;

    [SerializeField] private List<Image> imagesStown = new();
    [SerializeField] private TextMeshProUGUI textTask;
    [SerializeField] private List<TaskVisualCell> cells = new();

    private TaskStatus _status = TaskStatus.Active;
    private ITaskCondition _taskCondition = null;
    private int _id;

    public void Initialize(int id)
    {
        _id = id;

        cells.ForEach(data => data.OnChoose += ChooseCell);
    }

    public void Dispose()
    {
        _taskCondition.OnTaskConditionMet_CellIndexes -= ActivateWinCells;
    }

    public void SetData(Sprite spriteStown, ITaskCondition taskCondition)
    {
        _taskCondition = taskCondition;

        _taskCondition.OnTaskConditionMet_CellIndexes += ActivateWinCells;

        imagesStown.ForEach(data => data.sprite = spriteStown);

        textTask.text = _taskCondition.TaskSmallDescription;
    }

    public void SetNumberValue(int cell, NumberValue numberValue, Color colorText)
    {
        cells.FirstOrDefault(data => data.Id == cell).SetData(numberValue, colorText);

        CheckTask();
    }

    public void ActivateCells()
    {
        cells.ForEach(data => data.Activate());
    }

    public void DeactivateCells()
    {
        cells.ForEach(data => data.Deactivate());
    }

    private void ActivateWinCells(List<int> cellsIndexes)
    {
        for (int i = 0; i < cellsIndexes.Count; i++)
        {
            cells.FirstOrDefault(data => data.Id == cellsIndexes[i]).ActivateWin();
        }
    }



    private void CheckTask()
    {
        Dictionary<int, NumberValue> usedCells = new();

        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].CurrentNumberValue != null)
            {
                usedCells.Add(i, cells[i].CurrentNumberValue);
            }
        }

        Debug.Log(usedCells.Count);

        bool isFull = _taskCondition.NeedCountNumber == usedCells.Count;

        if (_taskCondition.IsMet(usedCells))
        {
            Debug.Log("WIN");

            OnSuccess?.Invoke(_id);
        }
        else
        {
            if (isFull)
            {
                Debug.Log("LOSE");

                OnFail?.Invoke(_id);
            }
        }
    }

    #region Output

    public event Action<int, int> OnChooseCell;

    public event Action<int> OnChooseTask;

    public event Action<int> OnSuccess;
    public event Action<int> OnFail;


    private void ChooseCell(int index)
    {
        OnChooseCell?.Invoke(_id, index);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnChooseTask?.Invoke(_id);
    }

    #endregion
}

public enum TaskStatus
{
    Active,
    Inactive,
    Claimable,
    Completed,
    Failed
}
