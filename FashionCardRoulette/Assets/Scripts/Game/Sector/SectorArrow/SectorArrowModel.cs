using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorArrowModel
{
    private readonly IStoreNumberProvider _storeNumberProvider;

    public SectorArrowModel(IStoreNumberProvider storeNumberProvider)
    {
        _storeNumberProvider = storeNumberProvider;
    }

    public void ActivateZone()
    {
        OnActivateZone?.Invoke();
    }

    public void DeactivateZone()
    {
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
