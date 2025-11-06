using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseGenderModel
{
    private readonly IStoreCharacterProvider _storeCharacterProvider;
    private readonly ISoundProvider _soundProvider;

    public ChooseGenderModel(IStoreCharacterProvider storeCharacterProvider, ISoundProvider soundProvider)
    {
        _storeCharacterProvider = storeCharacterProvider;
        _soundProvider = soundProvider;
    }

    private int _currentGender = 0;

    public void SetGender(int id)
    {
        if(_currentGender == id) return;

        OnDeactivate?.Invoke(_currentGender);

        _currentGender = id;
        OnActivate?.Invoke(_currentGender);

        _soundProvider.PlayOneShot("Toggle");
    }

    public void SubmitChoice()
    {
        switch (_currentGender)
        {
            case 0:
                _storeCharacterProvider.SelectPersonsByGender(Gender.Man);
                OnChooseGender?.Invoke(Gender.Man);
                break;
            case 1:
                _storeCharacterProvider.SelectPersonsByGender(Gender.Woman);
                OnChooseGender?.Invoke(Gender.Woman);
                break;
        }

        _soundProvider.PlayOneShot("Click");
        Debug.Log(_currentGender);
    }

    #region Output

    public event Action<int> OnActivate;
    public event Action<int> OnDeactivate;

    public event Action<Gender> OnChooseGender;

    #endregion
}
