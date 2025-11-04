using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SectorArrowView : View
{
    [SerializeField] private SectorZone sectorZone;
    [SerializeField] private List<Transform> points = new();
    [SerializeField] private List<Sector> zones = new();

    [SerializeField] private Transform transformArrow;
    [SerializeField] private float speedMove = 2f;

    private Tween tweenMove;
    private int currentZoneIndex = 0;

    public void Initialize()
    {
        sectorZone.OnClickToZone += ClickToZone;
    }

    public void Dispose()
    {
        sectorZone.OnClickToZone -= ClickToZone;
    }

    public void ActivateZone()
    {
        sectorZone.Activate();

        StartMoveArrow();
    }

    public void DeactivateZone()
    {
        sectorZone.Deactivate();

        StopMoveArrow();
    }

    private void StartMoveArrow()
    {
        tweenMove?.Kill();

        Vector3[] path = new Vector3[points.Count];

        for (int i = 0; i < points.Count; i++)
            path[i] = points[i].localPosition;

        transformArrow.localPosition = path[0];
        currentZoneIndex = 0;
        OnSectorZoneChanged?.Invoke(currentZoneIndex);

        tweenMove = transformArrow
            .DOLocalPath(path, path.Length / speedMove, PathType.Linear)
            .SetOptions(true)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear)
            .OnUpdate(CheckCurrentZone);
    }

    private void StopMoveArrow()
    {
        tweenMove?.Kill();
    }

    private void CheckCurrentZone()
    {
        float arrowX = transformArrow.localPosition.x;

        for (int i = 0; i < zones.Count; i++)
        {
            float left = points[zones[i].LeftIndex].localPosition.x;
            float right = points[zones[i].RightIndex].localPosition.x;

            if(arrowX >= left && arrowX <= right)
            {
                if(currentZoneIndex != i)
                {
                    currentZoneIndex = i;
                    OnSectorZoneChanged?.Invoke(currentZoneIndex);
                    Debug.Log($"ZONE: {currentZoneIndex}");
                }
                return;
            }
        }

        currentZoneIndex = -1;
    }

    #region Output

    public event Action<int> OnSectorZoneChanged;

    public event Action OnClickToZone;

    private void ClickToZone()
    {
        OnClickToZone?.Invoke();
    }

    #endregion
}

[System.Serializable]
public class Sector
{
    [SerializeField] private int leftIndex;
    [SerializeField] private int rightIndex;

    public int LeftIndex => leftIndex;
    public int RightIndex => rightIndex;
}
