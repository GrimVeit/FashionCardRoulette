using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_Game : MovePanel
{
    [SerializeField] private Button buttonCharacter;
    [SerializeField] private Button buttonSpin;

    [SerializeField] private UIEffectCombination effectCombination;

    public override void Initialize()
    {
        base.Initialize();

        buttonCharacter.onClick.AddListener(() => OnClickToCharacter?.Invoke());
        buttonSpin.onClick.AddListener(() => OnClickToSpin?.Invoke());

        effectCombination.Initialize();
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonCharacter.onClick.RemoveListener(() => OnClickToCharacter?.Invoke());
        buttonSpin.onClick.RemoveListener(() => OnClickToSpin?.Invoke());

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

    public event Action OnClickToCharacter;
    public event Action OnClickToSpin;

    #endregion
}
