using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskDescriptionView : View
{
    [SerializeField] private TaskStatusNames taskStatusNames;
    [SerializeField] private TaskTypeNames taskTypeNames;

    [Header("Main")]
    [SerializeField] private TextMeshProUGUI textTaskDescription;
    [SerializeField] private TextMeshProUGUI textTaskType;
    [SerializeField] private TextMeshProUGUI textTaskStatus;
    [SerializeField] private TextMeshProUGUI textClaim;

    [Header("Background")]
    [SerializeField] private Image imageBackground;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteInactive;

    [Space]
    [SerializeField] private Button buttonClaim;
    [SerializeField] private UIEffect effect_ButtonClaim;

    private int _currentTaskId = -1;
    private int _claimCoins = 0;

    public void Initialize()
    {
        buttonClaim.onClick.AddListener(() => OnClaim?.Invoke(_currentTaskId, _claimCoins));

        effect_ButtonClaim.Initialize();
    }

    public void Dispose()
    {
        buttonClaim.onClick.RemoveListener(() => OnClaim?.Invoke(_currentTaskId, _claimCoins));

        effect_ButtonClaim.Dispose();
    }

    public void SetTask((TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition, int TaskId) task)
    {
        var description = task.TaskCondition.TaskFullDescription;
        textTaskDescription.text = description;

        var type = taskTypeNames.GetNameType(task.TaskType);
        textTaskType.text = type;

        var status = taskStatusNames.GetNameStatus(task.Status);
        textTaskStatus.text = status;

        _claimCoins = task.TaskCondition.ClaimCoins;
        textClaim.text = "+" + _claimCoins.ToString();

        _currentTaskId = task.TaskId;

        switch (task.Status)
        {
            case TaskStatus.InProgress:
                imageBackground.sprite = spriteActive;
                DeactivateClaimButton();
                break;
            case TaskStatus.Claimable:
                imageBackground.sprite = spriteActive;
                ActivateClaimButton();
                break;
            case TaskStatus.Completed:
                imageBackground.sprite = spriteActive;
                DeactivateClaimButton();
                break;
            case TaskStatus.Failed:
                imageBackground.sprite = spriteInactive;
                DeactivateClaimButton();
                break;
        }
    }

    private void ActivateClaimButton()
    {
        effect_ButtonClaim.ResetEffect();
        effect_ButtonClaim.ActivateEffect();
    }

    private void DeactivateClaimButton()
    {
        effect_ButtonClaim.DeactivateEffect();
    }

    #region Output

    public event Action<int, int> OnClaim;

    #endregion
}

#region TaskStatusName

[System.Serializable]
public class TaskStatusNames
{
    [SerializeField] private List<TaskStatusName> statusNames = new();

    public string GetNameStatus(TaskStatus taskStatus)
    {
        return statusNames.FirstOrDefault(data => data.TaskStatus == taskStatus).NameStatus;
    }
}

[System.Serializable]
public class TaskStatusName
{
    [SerializeField] private TaskStatus taskStatus;
    [SerializeField] private string nameStatus;

    public TaskStatus TaskStatus => taskStatus;
    public string NameStatus => nameStatus;
}

#endregion

#region TaskTypeName

[System.Serializable]
public class TaskTypeNames
{
    [SerializeField] private List<TaskTypeName> typesNames = new();

    public string GetNameType(TaskType taskType)
    {
        return typesNames.FirstOrDefault(data => data.TaskType == taskType).NameType;
    }
}

[System.Serializable]
public class TaskTypeName
{
    [SerializeField] private TaskType taskType;
    [SerializeField] private string nameType;

    public TaskType TaskType => taskType;
    public string NameType => nameType;
}

#endregion
