using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPanel_Game : MovePanel
{
    [SerializeField] private Button buttonExitPanel;

    public override void Initialize()
    {
        base.Initialize();

        buttonExitPanel.onClick.AddListener(() => OnClickToExit?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonExitPanel.onClick.RemoveListener(() => OnClickToExit?.Invoke());
    }

    #region Output

    public event Action OnClickToExit;

    #endregion
}
