using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberTrashModel
{
    public event Action OnMoveToTrash;

    private bool isTrashActive = true;

    public void MoveToTrash()
    {
        if(!isTrashActive) return;

        OnMoveToTrash?.Invoke();

        isTrashActive = false;
    }
}
