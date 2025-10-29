using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberTrashView : View
{
    [SerializeField] private Button buttonTrash;
    [SerializeField] private UIEffect effect_Button;

    public void Initialize()
    {
        buttonTrash.onClick.AddListener(ClickToTrash);

        effect_Button.Initialize();
        effect_Button.ActivateEffect();
    }

    public void Dispose()
    {
        buttonTrash.onClick.RemoveListener(ClickToTrash);

        effect_Button.Dispose();
    }

    public void Close()
    {
        effect_Button.ResetEffect();
    }

    #region Output

    public event Action OnClickToTrash;

    private void ClickToTrash()
    {
        OnClickToTrash?.Invoke();
    }

    #endregion
}
