using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NumberValue", menuName = "Game/Number/New")]
public class NumberValue : ScriptableObject
{
    [SerializeField] private int number;
    [SerializeField] private ColorNumber color;

    public int Number => number;
    public ColorNumber Color => color;
}
