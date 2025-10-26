using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskVisual : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TaskStatus TaskStatus => _status;
    public TaskType Type => _taskCondition.TaskType;
    public int Id => _id;

    [SerializeField] private TextMeshProUGUI textStatus;
    [SerializeField] private List<Image> imagesStown = new();
    [SerializeField] private TextMeshProUGUI textTask;
    [SerializeField] private List<TaskVisualCell> cells = new();

    [Header("Background")]
    [SerializeField] private Image imageBackground;
    [SerializeField] private Sprite sprite_Good;
    [SerializeField] private Sprite sprite_Bad;

    private TaskStatus _status = TaskStatus.InProgress;
    private ITaskCondition _taskCondition = null;
    private int _id;
    private bool isActiveInteraction = false;

    private Sequence _scaleSequence;

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


    public void ActivateInteractionTask()
    {
        isActiveInteraction = true;

        if (_status == TaskStatus.Claimable)
        {
            ActivateAnimationBubble();
        }
    }

    public void DeactivateInteractionTask()
    {
        isActiveInteraction = false;

        if (_status == TaskStatus.Claimable)
        {
            DeactivateAnimationBubble();
        }
    }


    public void SetTaskInProgress()
    {
        _status = TaskStatus.InProgress;

        imageBackground.sprite = sprite_Good;

        textStatus.text = "In Progress";
    }

    public void SetTaskClaimable()
    {
        _status = TaskStatus.Claimable;

        imageBackground.sprite = sprite_Good;

        textStatus.text = "Claimable";

        ActivateAnimationBubble();
    }

    public void SetTaskCompleted()
    {
        _status = TaskStatus.Completed;

        imageBackground.sprite = sprite_Good;

        textStatus.text = "Completed";
    }

    public void SetTaskFailed()
    {
        _status = TaskStatus.Failed;

        imageBackground.sprite = sprite_Bad;

        textStatus.text = "Failed";
    }

    private void ActivateAnimationBubble()
    {
        _scaleSequence?.Kill();

        _scaleSequence = DOTween.Sequence();

        _scaleSequence
            .Append(transform.DOScale(1.1f, 0.4f))
            .Append(transform.DOScale(1, 0.4f))
            .SetLoops(-1);
    }

    private void DeactivateAnimationBubble()
    {
        _scaleSequence?.Kill();

        _scaleSequence = DOTween.Sequence();

        _scaleSequence.Append(transform.DOScale(1, 0.4f));
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
        if (_status != TaskStatus.InProgress) return; 

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

            OnSuccessTask?.Invoke(_id);
        }
        else
        {
            if (isFull)
            {
                Debug.Log("LOSE");

                OnFailTask?.Invoke(_id);
            }
        }
    }

    #region Output

    public event Action<int, int> OnChooseCell;

    public event Action<int> OnChooseTask;

    public event Action<int> OnSuccessTask;
    public event Action<int> OnFailTask;


    private void ChooseCell(int index)
    {
        OnChooseCell?.Invoke(_id, index);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isActiveInteraction) return;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!isActiveInteraction) return;

        OnChooseTask?.Invoke(_id);
    }

    #endregion
}

public enum TaskStatus
{
    InProgress,
    Claimable,
    Completed,
    Failed
}
