using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberSelectionVisual : MonoBehaviour
{
    public int Id => id;

    [SerializeField] private int id;
    [SerializeField] private TextMeshProUGUI textNumber;

    public void SetData(int number)
    {
        textNumber.text = number.ToString();
    }
}
