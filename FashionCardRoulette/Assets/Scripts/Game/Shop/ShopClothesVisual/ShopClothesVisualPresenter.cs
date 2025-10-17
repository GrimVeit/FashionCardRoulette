using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopClothesVisualPresenter
{
    private readonly ShopClothesVisualModel _model;
    private readonly ShopClothesVisualView _view;

    public ShopClothesVisualPresenter(ShopClothesVisualModel model, ShopClothesVisualView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChooseToBuy += _model.ChooseShopClothes;

        _model.OnSetOpenClothes += _view.SetOpenClothes;
        _model.OnSetCloseClothes += _view.SetCloseClothes;
        _model.OnChangeClothesType += _view.ChangeClothesType;
        _model.OnEndChangeClothesType += _view.EndChangeClothesType;

        _model.OnActivate += _view.ActivateToggle;
        _model.OnDeactivate += _view.DeactivateToggle;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseToBuy -= _model.ChooseShopClothes;

        _model.OnSetOpenClothes -= _view.SetOpenClothes;
        _model.OnSetCloseClothes -= _view.SetCloseClothes;
        _model.OnChangeClothesType -= _view.ChangeClothesType;
        _model.OnEndChangeClothesType -= _view.EndChangeClothesType;

        _model.OnActivate -= _view.ActivateToggle;
        _model.OnDeactivate -= _view.DeactivateToggle;
    }


}
