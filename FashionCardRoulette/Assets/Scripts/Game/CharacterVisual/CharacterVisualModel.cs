using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisualModel
{
    private readonly IChooseCharacterEventsProvider _chooseCharacterEventsProvider;

    public CharacterVisualModel(IChooseCharacterEventsProvider chooseCharacterEventsProvider)
    {
        _chooseCharacterEventsProvider = chooseCharacterEventsProvider;
        _chooseCharacterEventsProvider.OnChooseCharacter += SetCharacter;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _chooseCharacterEventsProvider.OnChooseCharacter -= SetCharacter;
    }

    private void SetCharacter(Gender gender, int id)
    {
        OnSetCharacter?.Invoke(gender, id);
    }

    #region Output

    public event Action<Gender, int> OnSetCharacter;

    #endregion
}
