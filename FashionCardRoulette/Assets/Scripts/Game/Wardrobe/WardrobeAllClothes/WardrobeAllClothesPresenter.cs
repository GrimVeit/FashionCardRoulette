using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeAllClothesPresenter
{
    private readonly WardrobeAllClothesModel _model;
    private readonly WardrobeAllClothesView _view;

    public WardrobeAllClothesPresenter(WardrobeAllClothesModel model, WardrobeAllClothesView view)
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
        DeactivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnSetClothes += _view.SetClothes;
    }

    private void DeactivateEvents()
    {
        _model.OnSetClothes -= _view.SetClothes;
    }
}
