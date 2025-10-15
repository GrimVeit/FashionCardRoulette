using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClothesVisualModel
{
    private readonly IChooseGenderClothesEventsProvider _chooseGenderClothesEventsProvider;

    private GenderClothesTypes _currentGenderClothesType;

    public ClothesVisualModel(IChooseGenderClothesEventsProvider chooseGenderClothesEventsProvider)
    {
        _chooseGenderClothesEventsProvider = chooseGenderClothesEventsProvider;
        _chooseGenderClothesEventsProvider.OnChooseGenderClothesType += SetGenderClothesTypes;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _chooseGenderClothesEventsProvider.OnChooseGenderClothesType -= SetGenderClothesTypes;
    }

    public void SetClothes(ClothesType type, int id)
    {
        if(_currentGenderClothesType == null) return;

        if (_currentGenderClothesType.IsHaveClothesType(type))
        {
            OnSetClothes?.Invoke(type, id);
        }
    }

    private void SetGenderClothesTypes(GenderClothesTypes genderClothesType)
    {
        _currentGenderClothesType = genderClothesType;
    }

    #region Output

    public event Action<ClothesType, int> OnSetClothes;

    #endregion
}

public enum ClothesType
{
    None, 
    Woman_Outerwear, Woman_Shoe, Woman_Jewerly, Woman_Glasses, Woman_Hat,
    Man_Outerwear, Man_Shoe, Man_Clock, Man_Beard, Man_Glasses
}
