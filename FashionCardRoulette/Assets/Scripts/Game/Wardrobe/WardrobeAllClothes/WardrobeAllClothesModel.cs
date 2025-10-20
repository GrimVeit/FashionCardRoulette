using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeAllClothesModel
{
    private readonly IStoreClothesEventsProvider _storeClothesEventsProvider;

    public WardrobeAllClothesModel(IStoreClothesEventsProvider storeClothesEventsProvider)
    {
        _storeClothesEventsProvider = storeClothesEventsProvider;
    }

    public void Initialize()
    {
        _storeClothesEventsProvider.OnChooseOpenClothes += SetClothes;
    }

    public void Dispose()
    {
        _storeClothesEventsProvider.OnChooseOpenClothes += SetClothes;
    }

    private void SetClothes(Clothes clothes)
    {
        OnSetClothes?.Invoke(clothes);
    }

    #region Output

    public event Action<Clothes> OnSetClothes;

    #endregion
}
