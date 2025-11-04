using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteStateModel
{
    public void SetGame_Smooth()
    {
        OnSetGame_Smooth?.Invoke();
    }

    public void SetIdle_Smooth()
    {
        OnSetIdle_Smooth?.Invoke();
    }

    public void SetIddle()
    {
        OnSetIdle?.Invoke();
    }

    public event Action OnSetGame_Smooth;
    public event Action OnSetIdle_Smooth;
    public event Action OnSetIdle;
}
