using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeClothesVisualPresenter : IWardrobeClothesEventsProvider
{
    private readonly WardrobeClothesVisualModel _model;
    private readonly WardrobeClothesVisualView _view;

    public WardrobeClothesVisualPresenter(WardrobeClothesVisualModel model, WardrobeClothesVisualView view)
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
        _view.OnChooseToSelect += _model.SetChooseClothes;
        _view.OnSubmitChoice += _model.SubmitChoice;

        _model.OnSetSelectClothes += _view.SetSelectClothes;
        _model.OnSetDeselectClothes += _view.SetDeselectClothes;
        _model.OnChangeClothesType += _view.ChangeClothesType;
        _model.OnEndChangeClothesType += _view.EndChangeClothesType;

        _model.OnActivate += _view.ActivateToggle;
        _model.OnDeactivate += _view.DeactivateToggle;

        _model.OnActivateSubmit += _view.ActivateSubmit;
        _model.OnDeactivateSubmit += _view.DeactivateSubmit;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseToSelect -= _model.SetChooseClothes;
        _view.OnSubmitChoice -= _model.SubmitChoice;

        _model.OnSetSelectClothes -= _view.SetSelectClothes;
        _model.OnSetDeselectClothes -= _view.SetDeselectClothes;
        _model.OnChangeClothesType -= _view.ChangeClothesType;
        _model.OnEndChangeClothesType -= _view.EndChangeClothesType;

        _model.OnActivate -= _view.ActivateToggle;
        _model.OnDeactivate -= _view.DeactivateToggle;

        _model.OnActivateSubmit -= _view.ActivateSubmit;
        _model.OnDeactivateSubmit -= _view.DeactivateSubmit;
    }

    #region Output

    public event Action OnSubmitSelect
    {
        add => _model.OnSubmitSelect += value;
        remove => _model.OnSubmitSelect -= value;
    }

    #endregion
}

public interface IWardrobeClothesEventsProvider
{
    public event Action OnSubmitSelect;
}
