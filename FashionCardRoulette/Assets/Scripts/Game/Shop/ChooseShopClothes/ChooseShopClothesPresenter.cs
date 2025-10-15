using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseShopClothesPresenter
{
    private readonly ChooseShopClothesModel _model;
    private readonly ChooseShopClothesView _view;

    public ChooseShopClothesPresenter(ChooseShopClothesModel model, ChooseShopClothesView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
    }

    public void Dispose()
    {
        DeacxtivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChooseType += _model.ChooseType;

        _model.OnChooseGenderClothesTypes += _view.SetShopClothesType;
    }

    private void DeacxtivateEvents()
    {
        _view.OnChooseType -= _model.ChooseType;

        _model.OnChooseGenderClothesTypes -= _view.SetShopClothesType;
    }
}
