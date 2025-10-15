using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesVisualPresenter
{
    private readonly ClothesVisualModel _model;
    private readonly ClothesVisualView _view;

    public ClothesVisualPresenter(ClothesVisualModel model, ClothesVisualView view)
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
