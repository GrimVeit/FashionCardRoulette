using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseGenderClothesPresenter : IChooseGenderClothesEventsProvider
{
    private readonly ChooseGenderClothesModel _model;

    public ChooseGenderClothesPresenter(ChooseGenderClothesModel model)
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

    #region Output

    public event Action<GenderClothesTypes> OnChooseGenderClothesType
    {
        add => _model.OnChooseGenderClothesTypes += value;
        remove => _model.OnChooseGenderClothesTypes -= value;
    }

    #endregion
}

public interface IChooseGenderClothesEventsProvider
{
    public event Action<GenderClothesTypes> OnChooseGenderClothesType;
}
