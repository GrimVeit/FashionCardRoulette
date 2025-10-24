using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_Game : MovePanel
{
    [SerializeField] private Button buttonCharacter;
    [SerializeField] private Button buttonSpin;

    public override void Initialize()
    {
        base.Initialize();

        buttonCharacter.onClick.AddListener(() => OnClickToCharacter?.Invoke());
        buttonSpin.onClick.AddListener(() => OnClickToSpin?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonCharacter.onClick.RemoveListener(() => OnClickToCharacter?.Invoke());
        buttonSpin.onClick.RemoveListener(() => OnClickToSpin?.Invoke());
    }

    #region Output

    public event Action OnClickToCharacter;
    public event Action OnClickToSpin;

    #endregion
}
