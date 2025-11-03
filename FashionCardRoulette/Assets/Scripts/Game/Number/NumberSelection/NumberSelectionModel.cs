using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSelectionModel
{
    private readonly List<int> _allNumbers = new()
    {
        0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36,
        11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9,
        22, 18, 29, 7, 28, 12, 35, 3, 26
    };

    public void SelectNumbers(int number)
    {
        int index = _allNumbers.IndexOf(number);

        if(index == -1)
        {
            Debug.LogError("Not found number - " + number);
            return;
        }

        List<int> result = new();
        int count = _allNumbers.Count;

        for (int i = -3; i <= 3; i++)
        {
            int wrappedIndex = (index + i + count) % count;
            result.Add(_allNumbers[wrappedIndex]);
        }

        OnSelectSevenNumbers?.Invoke(result);
    }

    public void ActivateChoose()
    {
        OnActivate?.Invoke();
    }

    public void DeactivateChoose()
    {
        OnDeactivate?.Invoke();
    }

    #region Output

    public event Action<List<int>> OnSelectSevenNumbers;


    public event Action OnActivate;
    public event Action OnDeactivate;

    #endregion
}
