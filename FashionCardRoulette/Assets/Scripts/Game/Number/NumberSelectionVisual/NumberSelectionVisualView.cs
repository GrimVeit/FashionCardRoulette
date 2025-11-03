using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class NumberSelectionVisualView : View
{
    [SerializeField] private List<NumberSelectionVisual> selectionVisuals = new();
    [SerializeField] private NumberSelectionTexts numberSelectionTexts;
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

    public void SetSevenNumbers(List<int> numbers)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            selectionVisuals[i].SetData(numbers[i]);

            numberSelectionTexts.SetNumber(i, numbers[i]);
        }

        effectCombination.ActivateEffect();
    }
}

[System.Serializable]
public class NumberSelectionTexts
{
    [SerializeField] private List<NumberSelectionText> texts = new();

    public void SetNumber(int index, int number)
    {
        texts.FirstOrDefault(data => data.Index == index).SetNumber(number);
    }
}

[System.Serializable]
public class NumberSelectionText
{
    [SerializeField] private int index;
    [SerializeField] private List<TextMeshProUGUI> textNumbers;

    public int Index => index;

    public void SetNumber(int number)
    {
        textNumbers.ForEach(data => data.text = number.ToString());
    }
}
