using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeFitClothesModel
{
    private readonly IStoreClothesEventsProvider _storeClothesEventsProvider;

    public WardrobeFitClothesModel(IStoreClothesEventsProvider storeClothesEventsProvider)
    {
        _storeClothesEventsProvider = storeClothesEventsProvider;

        _storeClothesEventsProvider.OnSelectClothes += SetClothes;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _storeClothesEventsProvider.OnSelectClothes -= SetClothes;
    }

    #region Output

    public event Action<Clothes> OnSetClothes;

    private void SetClothes(Clothes clothes)
    {
        OnSetClothes?.Invoke(clothes);
    }

    #endregion
}
