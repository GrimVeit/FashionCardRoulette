using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SectorArrowView : View
{
    [SerializeField] private List<Transform> points = new();
    [SerializeField] private List<SectorZone> zones = new();

    [SerializeField] private Transform transformArrow;
    [SerializeField] private float speedMove = 2f;

    private Tween tweenMove;
    private int currentZoneIndex = 0;

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void StartMoveArrow()
    {
        Vector3[] path = new Vector3[points.Count];

        for (int i = 0; i < points.Count; i++)
            path[i] = points[i].localPosition;

        tweenMove = transformArrow
            .DOLocalPath(path, path.Length / speedMove, PathType.CatmullRom)
            .SetOptions(true)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear)
            .OnUpdate(CheckCurrentZone);
    }

    public void StopMoveArrow()
    {
        tweenMove.Kill();
        OnSectorZoneSubmit?.Invoke(currentZoneIndex);
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
            }

            return;
        }

        currentZoneIndex = -1;
    }

    #region Output

    public event Action<int> OnSectorZoneChanged;
    public event Action<int> OnSectorZoneSubmit;

    #endregion
}

[System.Serializable]
public class SectorZone
{
    [SerializeField] private int leftIndex;
    [SerializeField] private int rightIndex;

    public int LeftIndex => leftIndex;
    public int RightIndex => rightIndex;
}
