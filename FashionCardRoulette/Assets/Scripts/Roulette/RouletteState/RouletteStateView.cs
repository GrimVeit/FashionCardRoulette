using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RouletteStateView : View
{
    [SerializeField] private Transform transformRouletteMove;
    [SerializeField] private Transform transformRouletteScale;

    [SerializeField] private Transform transformIdle;
    [SerializeField] private Transform transformGame;

    [SerializeField] private Vector3 vectorScaleIdle;
    [SerializeField] private Vector3 vectorScaleGame;

    private Sequence _sequenceRoulette;

    public void SetIdle()
    {
        _sequenceRoulette?.Kill();

        transformRouletteMove.localPosition = transformIdle.localPosition;
        transformRouletteScale.localScale = vectorScaleIdle;
    }

    public void SetIdle_Smooth()
    {
        _sequenceRoulette?.Kill();

        _sequenceRoulette = DOTween.Sequence();

        _sequenceRoulette
            .Append(transformRouletteMove.DOLocalMove(transformIdle.localPosition, 0.3f))
            .Join(transformRouletteScale.DOScale(vectorScaleIdle, 0.3f));
    }

    public void SetGame_Smooth()
    {
        _sequenceRoulette?.Kill();

        _sequenceRoulette = DOTween.Sequence();

        _sequenceRoulette
            .Append(transformRouletteMove.DOLocalMove(transformGame.localPosition, 0.3f))
            .Join(transformRouletteScale.DOScale(vectorScaleGame, 0.3f));
    }
}
