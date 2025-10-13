using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseGenderPresenter
{
    private readonly ChooseGenderModel _model;
    private readonly ChooseGenderView _view;

    public ChooseGenderPresenter(ChooseGenderModel model, ChooseGenderView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChooseGender += _model.SetGender;
        _view.OnSubmit += _model.SubmitChoice;

        _model.OnActivate += _view.Activate;
        _model.OnDeactivate += _view.Deactivate;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseGender -= _model.SetGender;
        _view.OnSubmit -= _model.SubmitChoice;

        _model.OnActivate -= _view.Activate;
        _model.OnDeactivate -= _view.Deactivate;
    }
}

public enum Gender
{
    Man, Woman
}

