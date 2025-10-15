using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_Game : MovePanel
{
    [SerializeField] private Button buttonCharacter;

    public override void Initialize()
    {
        base.Initialize();

        buttonCharacter.onClick.AddListener(() => OnClickToCharacter?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonCharacter.onClick.RemoveListener(() => OnClickToCharacter?.Invoke());
    }

    #region Output

    public event Action OnClickToCharacter;

    #endregion
}
