using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseGenderModel
{
    private IStoreCharacterProvider _storeCharacterProvider;

    public ChooseGenderModel(IStoreCharacterProvider storeCharacterProvider)
    {
        _storeCharacterProvider = storeCharacterProvider;
    }

    private int _currentGender = 0;

    public void SetGender(int id)
    {
        OnDeactivate?.Invoke(_currentGender);

        _currentGender = id;
        OnActivate?.Invoke(_currentGender);
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

        Debug.Log(_currentGender);
    }

    #region Output

    public event Action<int> OnActivate;
    public event Action<int> OnDeactivate;

    public event Action<Gender> OnChooseGender;

    #endregion
}
