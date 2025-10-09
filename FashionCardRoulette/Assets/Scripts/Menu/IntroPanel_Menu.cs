using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroPanel_Menu : MovePanel
{
    [SerializeField] private List<UIEffectCombination> uIEffectCombinations = new List<UIEffectCombination>();

    public override void Initialize()
    {
        base.Initialize();

        uIEffectCombinations.ForEach(data => data.Initialize());
    }

    public override void Dispose()
    {
        base.Dispose();

        uIEffectCombinations.ForEach(data => data.Dispose());
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();
    }

    #region Output

    public event Action OnClickToPlay;

    #endregion
}
