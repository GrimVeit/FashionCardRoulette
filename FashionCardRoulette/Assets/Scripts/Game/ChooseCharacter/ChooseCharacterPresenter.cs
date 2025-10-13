using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacterPresenter : IChooseCharacterEventsProvider
{
    private readonly ChooseCharacterModel _model;
    private readonly ChooseCharacterView _view;

    public ChooseCharacterPresenter(ChooseCharacterModel model, ChooseCharacterView view)
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
        _view.OnChoose += _model.SetCharacter;
        _view.OnSubmitChoice += _model.SubmitChoice;

        _model.OnActivate += _view.Activate;
        _model.OnDeactivate += _view.Deactivate;
        _model.OnChoosePersons += _view.SetCharacters;
    }

    private void DeactivateEvents()
    {
        _view.OnChoose -= _model.SetCharacter;
        _view.OnSubmitChoice -= _model.SubmitChoice;

        _model.OnActivate -= _view.Activate;
        _model.OnDeactivate -= _view.Deactivate;
        _model.OnChoosePersons -= _view.SetCharacters;
    }

    #region Output

    public event Action<Gender, int> OnChooseCharacter
    {
        add => _model.OnChooseCharacter += value;
        remove => _model.OnChooseCharacter -= value;
    }

    #endregion
}

public interface IChooseCharacterEventsProvider
{
    public event Action<Gender, int> OnChooseCharacter;
}
