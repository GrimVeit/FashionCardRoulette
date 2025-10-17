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
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {

    }

    private void DeactivateEvents()
    {

    }

    
}
