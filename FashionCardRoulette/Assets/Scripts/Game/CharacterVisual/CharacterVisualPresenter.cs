using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisualPresenter
{
    private readonly CharacterVisualModel _model;
    private readonly CharacterVisualView _view;

    public CharacterVisualPresenter(CharacterVisualModel model, CharacterVisualView view)
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
        _model.OnSetCharacter += _view.SetCharacter;
    }

    private void DeactivateEvents()
    {
        _model.OnSetCharacter -= _view.SetCharacter;
    }
}
