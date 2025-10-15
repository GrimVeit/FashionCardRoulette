using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCharacterPresenter : IStoreCharacterProvider, IStoreCharacterEventsProvider
{
    private readonly StoreCharacterModel _model;

    public StoreCharacterPresenter(StoreCharacterModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Input

    public void SelectPersonsByGender(Gender gender)
    {
        _model.SelectPersonsByGender(gender);
    }

    #endregion

    #region Output

    public event Action<List<PersonZero>> OnChooseGender
    {
        add => _model.OnChooseGender += value;
        remove => _model.OnChooseGender -= value;
    }

    #endregion
}

public interface IStoreCharacterProvider
{
    public void SelectPersonsByGender(Gender gender);
}

public interface IStoreCharacterEventsProvider
{
    public event Action<List<PersonZero>> OnChooseGender;
}
