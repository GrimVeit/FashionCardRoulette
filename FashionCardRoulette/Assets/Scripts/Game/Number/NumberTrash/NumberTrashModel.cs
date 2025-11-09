using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberTrashModel
{
    public event Action OnMoveToTrash;

    private bool isTrashActive = true;

    private readonly ISoundProvider _soundProvider;

    public NumberTrashModel(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void MoveToTrash()
    {
        if(!isTrashActive) return;

        OnMoveToTrash?.Invoke();

        isTrashActive = false;

        _soundProvider.PlayOneShot("NumberTrash");
    }
}
