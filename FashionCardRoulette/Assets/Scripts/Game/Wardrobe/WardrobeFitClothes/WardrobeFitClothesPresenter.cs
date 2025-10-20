using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeFitClothesPresenter
{
    private readonly WardrobeFitClothesModel _model;
    private readonly WardrobeFitClothesView _view;

    public WardrobeFitClothesPresenter(WardrobeFitClothesModel model, WardrobeFitClothesView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
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
