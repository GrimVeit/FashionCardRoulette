using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class NumberBallVisualView : View
{
    [SerializeField] private Transform transformBall;
    [SerializeField] private Transform transformParent;
    [SerializeField] private List<RouletteSlotValue> slotValues;
    [SerializeField] private float timeMove;

    public void SetNumber(int number)
    {
        if(transformBall.transform.parent != transformParent) 
           transformBall.SetParent(transformParent);

        var value = slotValues.FirstOrDefault(x => x.NumberValue.Number == number);

        transformBall.localScale = Vector3.one;
        transformBall.DOLocalMove(value.SlotTransform.localPosition, timeMove);
    }
}
