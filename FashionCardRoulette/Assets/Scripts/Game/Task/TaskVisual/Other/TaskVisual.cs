using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskVisual : MonoBehaviour
{
    [SerializeField] private List<Image> imagesStown = new();
    [SerializeField] private TextMeshProUGUI textTask;

    public void SetData(Sprite spriteStown, string task)
    {
        imagesStown.ForEach(data => data.sprite = spriteStown);

        textTask.text = task;
    }
}
