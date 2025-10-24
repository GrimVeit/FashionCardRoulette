using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NumberValues", menuName = "Game/Number/New Group")]
public class NumberValues : ScriptableObject
{
    [SerializeField] private List<NumberValue> values = new List<NumberValue>();

    public NumberValue GetRandomNumberValue()
    {
        return values[Random.Range(0, values.Count)];
    }
}
