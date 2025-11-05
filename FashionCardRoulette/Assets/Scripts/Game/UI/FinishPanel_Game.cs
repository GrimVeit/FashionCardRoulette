using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishPanel_Game : MovePanel
{
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonExit;

    [SerializeField] private UIEffectCombination effectCombination;

    public override void Initialize()
    {
        base.Initialize();

        buttonRestart.onClick.AddListener(() => OnClickToRestart?.Invoke());
        buttonExit.onClick.AddListener(() => OnClickToExit?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonRestart.onClick.RemoveListener(() => OnClickToRestart?.Invoke());
        buttonExit.onClick.RemoveListener(() => OnClickToExit?.Invoke());
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

    public event Action OnClickToRestart;
    public event Action OnClickToExit;

    #endregion
}
