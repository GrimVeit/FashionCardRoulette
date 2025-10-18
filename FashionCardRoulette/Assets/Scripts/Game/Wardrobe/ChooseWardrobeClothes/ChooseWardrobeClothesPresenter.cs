using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWardrobeClothesPresenter
{
    private readonly ChooseWardrobeClothesModel _model;
    private readonly ChooseWardrobeClothesView _view;

    public ChooseWardrobeClothesPresenter(ChooseWardrobeClothesModel model, ChooseWardrobeClothesView view)
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

        _model.OnChooseGenderClothesTypes += _view.SetWardrobeClothesType;
    }

    private void DeacxtivateEvents()
    {
        _view.OnChooseType -= _model.ChooseType;

        _model.OnChooseGenderClothesTypes -= _view.SetWardrobeClothesType;
    }
}
