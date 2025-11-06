using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopClothesVisualModel
{
    private readonly IStoreClothesEventsProvider _storeClothesEventsProvider;
    private readonly IShopClothesProvider _shopClothesProvider;
    private readonly IShopClothesEventsProvider _shopClothesEventsProvider;

    private readonly ISoundProvider _soundProvider;

    public ShopClothesVisualModel(IStoreClothesEventsProvider storeClothesEventsProvider, IShopClothesProvider shopClothesProvider, IShopClothesEventsProvider shopClothesEventsProvider, ISoundProvider soundProvider)
    {
        _storeClothesEventsProvider = storeClothesEventsProvider;
        _shopClothesProvider = shopClothesProvider;
        _shopClothesEventsProvider = shopClothesEventsProvider;
        _soundProvider = soundProvider;

        _storeClothesEventsProvider.OnChooseCloseClothes += SetCloseClothes;
        _storeClothesEventsProvider.OnChooseOpenClothes += SetOpenClothes;
        _storeClothesEventsProvider.OnChangeChooseClothes += ClearClothes;
        _storeClothesEventsProvider.OnEndChangeChooseClothes += ClearEndClothes;

        _shopClothesEventsProvider.OnChooseClothes += ActivateClothes;
        _shopClothesEventsProvider.OnUnchooseClothes += DeactivateClothes;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _storeClothesEventsProvider.OnChooseCloseClothes -= SetCloseClothes;
        _storeClothesEventsProvider.OnChooseOpenClothes -= SetOpenClothes;
        _storeClothesEventsProvider.OnChangeChooseClothes -= ClearClothes;
        _storeClothesEventsProvider.OnEndChangeChooseClothes -= ClearEndClothes;

        _shopClothesEventsProvider.OnChooseClothes -= ActivateClothes;
        _shopClothesEventsProvider.OnUnchooseClothes -= DeactivateClothes;
    }

    public void LeftRight()
    {
        _soundProvider.PlayOneShot("Click");
    }

    public void ChooseShopClothes(Clothes clothes)
    {
        _soundProvider.PlayOneShot("Toggle");

        _shopClothesProvider.ChooseClothes(clothes);
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
        _shopClothesProvider.AllDelete();

        OnChangeClothesType?.Invoke(type);
    }

    private void ClearEndClothes()
    {
        OnEndChangeClothesType?.Invoke();
    }

    public event Action<Clothes> OnSetOpenClothes;
    public event Action<Clothes> OnSetCloseClothes;
    public event Action<ClothesType> OnChangeClothesType;
    public event Action OnEndChangeClothesType;

    #endregion

    public event Action<ClothesType, int> OnActivate;
    public event Action<ClothesType, int> OnDeactivate;
}
