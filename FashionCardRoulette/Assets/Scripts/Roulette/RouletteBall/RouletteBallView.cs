using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RouletteBallView : View, IIdentify
{
    public event Action<Vector3> OnBallStopped;
    public string GetID() => id;

    [SerializeField] private string id;
    [SerializeField] private Transform transformParent;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private Transform ball;
    [SerializeField] private Transform transformStart;
    [SerializeField] private Transform transformEnd;
    private float startRadius;
    private float endRadius;
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;
    [SerializeField] private float startSpeed;
    [SerializeField] private float endSpeed = 0;
    [SerializeField] private List<RouletteSlotValue> rouletteSlotValues = new List<RouletteSlotValue>();

    private float currentRadius;
    private float currentSpeed;
    private float angle;

    public void Initialize()
    {
        startRadius = Vector3.Distance(transformStart.localPosition, centerPoint.localPosition);
        endRadius = Vector3.Distance(transformEnd.localPosition, centerPoint.localPosition);
    }

    public void Dispose()
    {

    }

    public void StartSpin()
    {
        float value = UnityEngine.Random.Range(minDuration, maxDuration);

        Coroutines.Start(MoveBall());
        DOTween.To(() => currentRadius, x => currentRadius = x, endRadius, value);
        DOTween.To(() => currentSpeed, x => currentSpeed = x, endSpeed, value);
    }

    public void StartSpin(int number)
    {
        float value = UnityEngine.Random.Range(minDuration, maxDuration);

        Coroutines.Start(MoveBall(number));
        DOTween.To(() => currentRadius, x => currentRadius = x, endRadius, value);
        DOTween.To(() => currentSpeed, x => currentSpeed = x, endSpeed, value);
    }

    private IEnumerator MoveBall()
    {
        currentSpeed = startSpeed;
        currentRadius = startRadius;
        angle = 0f;

        ball.transform.SetParent(transformParent);

        while(currentRadius > endRadius)
        {
            angle += currentSpeed * Time.deltaTime;

            float x = centerPoint.position.x + Mathf.Cos(angle) * currentRadius;
            float y = centerPoint.position.y + Mathf.Sin(angle) * currentRadius;

            ball.transform.localPosition = new Vector3(x, y, ball.transform.localPosition.z);

            yield return null;
        }

        OnBallStopped?.Invoke(ball.transform.position);
    }

    private IEnumerator MoveBall(int number)
    {
        var needSlotValue = rouletteSlotValues.FirstOrDefault(data => data.NumberValue.Number == number);

        currentSpeed = startSpeed;
        currentRadius = startRadius;
        angle = 0f;

        ball.transform.SetParent(transformParent);

        while (currentRadius > endRadius)
        {
            angle += currentSpeed * Time.deltaTime;

            float x = centerPoint.position.x + Mathf.Cos(angle) * currentRadius;
            float y = centerPoint.position.y + Mathf.Sin(angle) * currentRadius;

            ball.transform.localPosition = new Vector3(x, y, ball.transform.localPosition.z);

            yield return null;
        }

        bool isChoose = false;

        while (!isChoose)
        {
            angle += currentSpeed * Time.deltaTime;

            float x = centerPoint.position.x + Mathf.Cos(angle) * currentRadius;
            float y = centerPoint.position.y + Mathf.Sin(angle) * currentRadius;

            ball.transform.localPosition = new Vector3(x, y, ball.transform.localPosition.z);

            RouletteSlotValue closestSlotValue = GetClosestSlot(ball.transform.position);

            if(needSlotValue == closestSlotValue)
            {
                isChoose = true;
                OnBallStopped?.Invoke(ball.transform.position);
                yield break;
            }

            yield return null;
        }
    }

    private RouletteSlotValue GetClosestSlot(Vector3 vector)
    {
        return rouletteSlotValues.OrderBy(rv => Vector3.Distance(vector, rv.SlotTransform.position)).First();
    }
}
