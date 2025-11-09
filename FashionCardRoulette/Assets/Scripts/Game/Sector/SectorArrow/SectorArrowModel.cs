using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorArrowModel
{
    private readonly IStoreNumberProvider _storeNumberProvider;

    private readonly ISoundProvider _soundProvider;

    public SectorArrowModel(IStoreNumberProvider storeNumberProvider, ISoundProvider soundProvider)
    {
        _storeNumberProvider = storeNumberProvider;
        _soundProvider = soundProvider;
    }

    public void ActivateZone()
    {
        OnActivateZone?.Invoke();
    }

    public void DeactivateZone()
    {
        _soundProvider.PlayOneShot("Click");

        OnDeactivateZone?.Invoke();
    }



    public void SetSectorZone(int zone)
    {
        _storeNumberProvider.SetSector(zone);
    }

    #region Output

    public event Action OnActivateZone;
    public event Action OnDeactivateZone;

    #endregion
}
