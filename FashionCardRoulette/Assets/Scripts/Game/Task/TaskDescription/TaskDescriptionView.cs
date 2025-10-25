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

    [Header("Background")]
    [SerializeField] private Image imageBackground;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteInactive;

    public void SetTask((TaskType TaskType, TaskStatus Status, ITaskCondition TaskCondition) task)
    {
        var description = task.TaskCondition.TaskFullDescription;
        textTaskDescription.text = description;

        var type = taskTypeNames.GetNameType(task.TaskType);
        textTaskType.text = type;

        var status = taskStatusNames.GetNameStatus(task.Status);
        textTaskStatus.text = status;


        if(task.Status != TaskStatus.Failed)
        {
            imageBackground.sprite = spriteActive;
        }
        else
        {
            imageBackground.sprite = spriteInactive;
        }
    }
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
