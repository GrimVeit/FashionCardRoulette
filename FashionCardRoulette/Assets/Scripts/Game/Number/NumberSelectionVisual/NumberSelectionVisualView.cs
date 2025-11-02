using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSelectionVisualView : View
{
    [SerializeField] private List<NumberSelectionVisual> selectionVisuals = new();
    [SerializeField] private UIEffectCombination effectCombination;

    public void Initialize()
    {
        effectCombination.Initialize();
        effectCombination.ActivateEffect();
    }

    public void Dispose()
    {
        effectCombination.Dispose();
    }

    public void SetFiveNumbers(List<int> numbers)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            selectionVisuals[i].SetData(numbers[i]);
        }

        effectCombination.ActivateEffect();
    }
}
