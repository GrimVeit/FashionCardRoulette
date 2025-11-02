using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NumberSelectionView : View
{
    [SerializeField] private List<NumberSelectionSection> sections = new();

    public void Initialize()
    {
        sections.ForEach(data => data.OnSelectSection += ChooseSection);

        var randomNumber = sections[Random.Range(0, sections.Count)].Number;
        OnChooseSection?.Invoke(randomNumber);
    }

    public void Dispose()
    {
        sections.ForEach(data => data.OnSelectSection -= ChooseSection);
    }

    public void Activate()
    {
        sections.ForEach(data => data.Activate());
    }

    public void Deactivate()
    {
        sections.ForEach(data => data.Deactivate());
    }

    #region Output

    public event Action<int> OnChooseSection;

    private void ChooseSection(int section)
    {
        OnChooseSection?.Invoke(section);
    }

    #endregion
}
