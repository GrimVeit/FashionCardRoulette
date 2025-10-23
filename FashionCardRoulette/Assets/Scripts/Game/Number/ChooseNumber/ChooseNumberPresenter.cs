using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseNumberPresenter
{
    private readonly ChooseNumberModel _model;
    private readonly ChooseNumberView _view;

    public ChooseNumberPresenter(ChooseNumberModel model, ChooseNumberView view)
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

public interface IChooseNumberProvider
{
    void SetNumber(int number);
}
