using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBallVisualPresenter
{
    private readonly NumberBallVisualModel _model;
    private readonly NumberBallVisualView _view;

    public NumberBallVisualPresenter(NumberBallVisualModel model, NumberBallVisualView view)
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
        _model.OnChooseNumber += _view.SetNumber;
    }

    private void DeactivateEvents()
    {
        _model.OnChooseNumber -= _view.SetNumber;
    }
}
