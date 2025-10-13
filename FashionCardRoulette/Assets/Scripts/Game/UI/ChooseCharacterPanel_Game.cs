using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCharacterPanel_Game : MovePanel
{
    [SerializeField] private Button buttonContinue;
    [SerializeField] private Button buttonBack;

    public override void Initialize()
    {
        base.Initialize();

        buttonContinue.onClick.AddListener(() => OnClickToContinue?.Invoke());
        buttonBack.onClick.AddListener(() => OnClickToBack?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonContinue.onClick.RemoveListener(() => OnClickToContinue?.Invoke());
        buttonBack.onClick.RemoveListener(() => OnClickToBack?.Invoke());
    }

    #region Output

    public event Action OnClickToContinue;
    public event Action OnClickToBack;

    #endregion
}
