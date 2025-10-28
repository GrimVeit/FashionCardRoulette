using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCharacterPanel_Game : MovePanel
{
    [SerializeField] private Button buttonContinue;
    [SerializeField] private Button buttonBack;

    [SerializeField] private UIEffectCombination effectCombination;

    public override void Initialize()
    {
        base.Initialize();

        buttonContinue.onClick.AddListener(() => OnClickToContinue?.Invoke());
        buttonBack.onClick.AddListener(() => OnClickToBack?.Invoke());

        effectCombination.Initialize();
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonContinue.onClick.RemoveListener(() => OnClickToContinue?.Invoke());
        buttonBack.onClick.RemoveListener(() => OnClickToBack?.Invoke());

        effectCombination.Dispose();
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        effectCombination.ActivateEffect();
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        effectCombination.DeactivateEffect();
    }

    #region Output

    public event Action OnClickToContinue;
    public event Action OnClickToBack;

    #endregion
}
