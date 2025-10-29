using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteSpinCountModel
{
    private readonly ITaskVisualInfoProvider _taskVisualInfoProvider;

    private int _countSpins;

    public RouletteSpinCountModel(ITaskVisualInfoProvider taskVisualInfoProvider)
    {
        _taskVisualInfoProvider = taskVisualInfoProvider;
    }

    public void Initialize()
    {
        _countSpins = _taskVisualInfoProvider.AllCountCells() + 1;

        OnChangeCountSpin?.Invoke(_countSpins);
    }

    public void Dispose()
    {

    }

    public void RemoveSpin()
    {
        if(_countSpins > 0)
        {
            _countSpins -= 1;
        }

        OnChangeCountSpin?.Invoke(_countSpins);

        if(_countSpins == 0)
        {
            OnEndSpins?.Invoke();
        }
    }

    #region Output

    public event Action<int> OnChangeCountSpin;

    public event Action OnEndSpins;

    #endregion
}
