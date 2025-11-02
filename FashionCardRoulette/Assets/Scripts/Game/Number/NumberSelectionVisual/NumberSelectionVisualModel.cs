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
        _numberSelectionEventsProvider.OnChooseFiveNumbers += ChooseFiveNumbers;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _numberSelectionEventsProvider.OnChooseFiveNumbers -= ChooseFiveNumbers;
    }

    #region Output

    public event Action<List<int>> OnChooseFiveNumbers;

    private void ChooseFiveNumbers(List<int> numbers)
    {
        if(_mainNumber == numbers[2]) return;

        _mainNumber = numbers[2];
        OnChooseFiveNumbers?.Invoke(numbers);
    }

    #endregion
}
