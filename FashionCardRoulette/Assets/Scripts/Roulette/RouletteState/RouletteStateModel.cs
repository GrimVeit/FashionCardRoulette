using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteStateModel
{
    public void SetGame()
    {
        OnSetGame?.Invoke();
    }

    public void SetIddle()
    {
        OnSetIdle?.Invoke();
    }

    public event Action OnSetGame;
    public event Action OnSetIdle;
}
