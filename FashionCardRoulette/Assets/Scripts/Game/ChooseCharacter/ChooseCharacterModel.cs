using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ChooseCharacterModel
{
    private readonly IStoreCharacterEventsProvider _storeCharacterEventsProvider;

    private (Gender gender, int id) _currentCharacter;

    public ChooseCharacterModel(IStoreCharacterEventsProvider storeCharacterEventsProvider)
    {
        _storeCharacterEventsProvider = storeCharacterEventsProvider;
    }

    public void Initialize()
    {
        _storeCharacterEventsProvider.OnChooseGender += ChoosePersons;
    }

    public void Dispose()
    {
        _storeCharacterEventsProvider.OnChooseGender -= ChoosePersons;
    }

    public void SetCharacter(Gender gender, int id)
    {
        OnDeactivate?.Invoke(_currentCharacter.gender, _currentCharacter.id);

        _currentCharacter = (gender, id);
        OnActivate?.Invoke(gender, id);
    }

    public void SubmitChoice()
    {
        Debug.Log(_currentCharacter.ToString());

        OnChooseCharacter?.Invoke(_currentCharacter.gender, _currentCharacter.id);
    }

    #region Output

    public event Action<List<PersonZero>> OnChoosePersons;
    public event Action<Gender, int> OnActivate;
    public event Action<Gender, int> OnDeactivate;
    public event Action<Gender, int> OnChooseCharacter;

    private void ChoosePersons(List<PersonZero> persons)
    {
        OnChoosePersons?.Invoke(persons);

        _currentCharacter = (persons[0].Gender, persons[0].ID);
        OnActivate?.Invoke(_currentCharacter.gender, _currentCharacter.id);
    }

    #endregion
}
