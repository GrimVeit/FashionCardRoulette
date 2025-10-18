using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopClothesPresenter : IShopClothesProvider, IShopClothesEventsProvider
{
    private readonly ShopClothesModel _model;
    private readonly ShopClothesView _view;

    public ShopClothesPresenter(ShopClothesModel model, ShopClothesView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnBuy += _model.SubmitBuy;

        _model.OnActivate += _view.Activate;
        _model.OnDeactivate += _view.Deactivate;
    }

    private void DeactivateEvents()
    {
        _view.OnBuy -= _model.SubmitBuy;

        _model.OnActivate -= _view.Activate;
        _model.OnDeactivate -= _view.Deactivate;
    }

    #region Output

    public event Action<Clothes> OnChooseClothes
    {
        add => _model.OnChooseClothes += value;
        remove => _model.OnChooseClothes -= value;
    }

    public event Action<Clothes> OnUnchooseClothes
    {
        add => _model.OnUnchooseClothes += value;
        remove => _model.OnUnchooseClothes -= value;
    }

    #endregion

    #region Input

    public void ChooseClothes(Clothes clothes)
    {
        _model.ChooseClothes(clothes);
    }

    public void AllDelete()
    {
        _model.AllDelete();
    }

    #endregion
}

public interface IShopClothesProvider
{
    public void ChooseClothes(Clothes clothes);
    public void AllDelete();
}

public interface IShopClothesEventsProvider
{
    public event Action<Clothes> OnChooseClothes;
    public event Action<Clothes> OnUnchooseClothes;
}
