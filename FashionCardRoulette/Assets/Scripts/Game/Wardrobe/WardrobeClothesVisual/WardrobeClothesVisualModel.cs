using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeClothesVisualModel
{
    private readonly IStoreClothesEventsProvider _storeClothesEventsProvider;

    public WardrobeClothesVisualModel(IStoreClothesEventsProvider storeClothesEventsProvider)
    {
        _storeClothesEventsProvider = storeClothesEventsProvider;

        _storeClothesEventsProvider.OnSelectClothes += SetSelectClothes;
        _storeClothesEventsProvider.OnDeselectClothes += SetDeselectClothes;

        _storeClothesEventsProvider.OnChangeChooseClothes += ClearClothes;
        _storeClothesEventsProvider.OnEndChangeChooseClothes += ClearEndClothes;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _storeClothesEventsProvider.OnSelectClothes -= SetSelectClothes;
        _storeClothesEventsProvider.OnDeselectClothes -= SetDeselectClothes;

        _storeClothesEventsProvider.OnChangeChooseClothes -= ClearClothes;
        _storeClothesEventsProvider.OnEndChangeChooseClothes -= ClearEndClothes;
    }

    public void SetChooseClothes(Clothes clothes)
    {

    }

    private void ActivateClothes(Clothes clothes)
    {
        OnActivate?.Invoke(clothes.ClothesType, clothes.Id);
    }

    private void DeactivateClothes(Clothes clothes)
    {
        OnDeactivate?.Invoke(clothes.ClothesType, clothes.Id);
    }

    #region Input

    private void SetSelectClothes(Clothes clothes)
    {
        OnSetSelectClothes?.Invoke(clothes);
    }

    private void SetDeselectClothes(Clothes clothes)
    {
        OnSetDeselectClothes?.Invoke(clothes);
    }


    private void ClearClothes(ClothesType type)
    {
        OnChangeClothesType?.Invoke(type);
    }

    private void ClearEndClothes()
    {
        OnEndChangeClothesType?.Invoke();
    }

    public event Action<Clothes> OnSetSelectClothes;
    public event Action<Clothes> OnSetDeselectClothes;

    public event Action<ClothesType> OnChangeClothesType;
    public event Action OnEndChangeClothesType;

    #endregion

    public event Action<ClothesType, int> OnActivate;
    public event Action<ClothesType, int> OnDeactivate;
}
