using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopClothesVisualModel
{
    private readonly IStoreClothesEventsProvider _storeClothesEventsProvider;

    public ShopClothesVisualModel(IStoreClothesEventsProvider storeClothesEventsProvider)
    {
        _storeClothesEventsProvider = storeClothesEventsProvider;

        _storeClothesEventsProvider.OnChooseCloseClothes += SetCloseClothes;
        _storeClothesEventsProvider.OnChooseOpenClothes += SetOpenClothes;
        _storeClothesEventsProvider.OnChangeChooseClothes += ClearClothes;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _storeClothesEventsProvider.OnChooseCloseClothes -= SetCloseClothes;
        _storeClothesEventsProvider.OnChooseOpenClothes -= SetOpenClothes;
        _storeClothesEventsProvider.OnChangeChooseClothes -= ClearClothes;
    }

    private void SetOpenClothes(Clothes clothes)
    {
        OnSetOpenClothes?.Invoke(clothes);
    }

    private void SetCloseClothes(Clothes clothes)
    {
        OnSetCloseClothes?.Invoke(clothes);
    }

    private void ClearClothes(ClothesType type)
    {
        OnChangeClothesType?.Invoke();
    }

    public event Action<Clothes> OnSetOpenClothes;
    public event Action<Clothes> OnSetCloseClothes;
    public event Action OnChangeClothesType;
}
