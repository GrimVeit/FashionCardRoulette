using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSelectionVisualModel
{
    private readonly INumberSelectionEventsProvider _numberSelectionEventsProvider;

    private int _mainNumber = -1;

    public NumberSelectionVisualModel(INumberSelectionEventsProvider numberSelectionEventsProvider)
    {
        _numberSelectionEventsProvider = numberSelectionEventsProvider;
        _numberSelectionEventsProvider.OnChooseSevenNumbers += ChooseSevenNumbers;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _numberSelectionEventsProvider.OnChooseSevenNumbers -= ChooseSevenNumbers;
    }

    #region Output

    public event Action<List<int>> OnChooseSevenNumbers;

    private void ChooseSevenNumbers(List<int> numbers)
    {
        if(_mainNumber == numbers[2]) return;

        _mainNumber = numbers[2];
        OnChooseSevenNumbers?.Invoke(numbers);
    }

    #endregion
}
