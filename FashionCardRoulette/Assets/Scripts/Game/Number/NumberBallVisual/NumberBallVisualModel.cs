using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBallVisualModel
{
    private readonly INumberSelectionEventsProvider _numberSelectionEventsProvider;

    public NumberBallVisualModel(INumberSelectionEventsProvider numberSelectionEventsProvider)
    {
        _numberSelectionEventsProvider = numberSelectionEventsProvider;

        _numberSelectionEventsProvider.OnChooseSevenNumbers += SetNumbers;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _numberSelectionEventsProvider.OnChooseSevenNumbers -= SetNumbers;
    }

    private void SetNumbers(List<int> numbers)
    {
        OnChooseNumber?.Invoke(numbers[3]);
    }

    #region Output

    public event Action<int> OnChooseNumber;

    #endregion
}
