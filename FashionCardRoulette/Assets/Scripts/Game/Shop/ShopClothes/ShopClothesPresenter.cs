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
        _view.OnChoose += _model.Choose;
        _view.OnCancel += _model.CancelBuy;
        _view.OnSubmitChoice += _model.SubmitBuy;

        _model.OnActivate += _view.Activate;
        _model.OnDeactivate += _view.Deactivate;

        _model.OnAddClothes += _view.SetClothes;
        _model.OnClearClothes += _view.Clear;

        _model.OnChangeAllPrice += _view.ChangeAllPrice;
    }

    private void DeactivateEvents()
    {
        _view.OnChoose -= _model.Choose;
        _view.OnCancel -= _model.CancelBuy;
        _view.OnSubmitChoice -= _model.SubmitBuy;

        _model.OnActivate -= _view.Activate;
        _model.OnDeactivate -= _view.Deactivate;

        _model.OnAddClothes -= _view.SetClothes;
        _model.OnClearClothes -= _view.Clear;

        _model.OnChangeAllPrice -= _view.ChangeAllPrice;
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

    public event Action OnCanBuy
    {
        add => _model.OnCanBuy += value;
        remove => _model.OnCanBuy -= value;
    }

    public event Action OnCannotBuy
    {
        add => _model.OnCannotBuy += value;
        remove => _model.OnCannotBuy -= value;
    }

    public event Action OnBuy
    {
        add => _model.OnBuy += value;
        remove => _model.OnBuy -= value;
    }

    public event Action OnCancelBuy
    {
        add => _model.OnCancelBuy += value;
        remove => _model.OnCancelBuy -= value;
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

    public event Action OnCanBuy;
    public event Action OnCannotBuy;
    public event Action OnBuy;
    public event Action OnCancelBuy;
}
