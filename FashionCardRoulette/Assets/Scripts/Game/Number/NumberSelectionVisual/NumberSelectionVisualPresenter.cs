using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSelectionVisualPresenter
{
    private readonly NumberSelectionVisualModel _model;
    private readonly NumberSelectionVisualView _view;

    public NumberSelectionVisualPresenter(NumberSelectionVisualModel model, NumberSelectionVisualView view)
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
        _model.OnChooseFiveNumbers += _view.SetFiveNumbers;
    }

    private void DeactivateEvents()
    {
        _model.OnChooseFiveNumbers -= _view.SetFiveNumbers;
    }
}
