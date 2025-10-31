using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TaskVisualMoveView : View
{
    [SerializeField] private Transform transformMove_1;
    [SerializeField] private Transform transformMove_2;
    [SerializeField] private Transform transformMove_3;
    [SerializeField] private Transform transformMove_4;

    [SerializeField] private Transform transformPosFinish_1;
    [SerializeField] private Transform transformPosFinish_2;
    [SerializeField] private Transform transformPosFinish_3;
    [SerializeField] private Transform transformPosFinish_4;

    [SerializeField] private float timeDuration;
    [SerializeField] private float timeAwait;

    private IEnumerator timer;

    public void SetFinish()
    {
        if(timer != null) Coroutines.Stop(timer);

        timer = MoveToFinish();
        Coroutines.Start(timer);
    }

    private IEnumerator MoveToFinish()
    {
        transformMove_1.DOLocalMove(transformPosFinish_1.localPosition, timeDuration);

        yield return new WaitForSeconds(timeAwait);

        transformMove_2.DOLocalMove(transformPosFinish_2.localPosition, timeDuration);

        yield return new WaitForSeconds(timeAwait);

        transformMove_3.DOLocalMove(transformPosFinish_3.localPosition, timeDuration);

        yield return new WaitForSeconds(timeAwait);

        transformMove_4.DOLocalMove(transformPosFinish_4.localPosition, timeDuration);
    }
}
