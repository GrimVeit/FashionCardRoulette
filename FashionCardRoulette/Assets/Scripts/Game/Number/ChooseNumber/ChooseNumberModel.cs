using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseNumberModel
{
    public void SetNumber(NumberValue numberValue)
    {
        OnSetNumber_Value?.Invoke(numberValue);

        OnSetNumber?.Invoke();
    }

    #region Output

    public event Action<NumberValue> OnSetNumber_Value;
    public event Action OnSetNumber;

    #endregion
}
