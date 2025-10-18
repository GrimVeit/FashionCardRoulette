using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClothesVisualModel
{
    private readonly IChooseGenderClothesEventsProvider _chooseGenderClothesEventsProvider;
    private readonly IStoreClothesEventsProvider _storeClothesEventsProvider;

    private GenderClothesTypes _currentGenderClothesType;

    public ClothesVisualModel(IChooseGenderClothesEventsProvider chooseGenderClothesEventsProvider, IStoreClothesEventsProvider storeClothesEventsProvider)
    {
        _chooseGenderClothesEventsProvider = chooseGenderClothesEventsProvider;
        _storeClothesEventsProvider = storeClothesEventsProvider;

        _chooseGenderClothesEventsProvider.OnChooseGenderClothesType += SetGenderClothesTypes;
        _storeClothesEventsProvider.OnSelectClothes += SetClothes;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _chooseGenderClothesEventsProvider.OnChooseGenderClothesType -= SetGenderClothesTypes;
        _storeClothesEventsProvider.OnSelectClothes -= SetClothes;
    }

    public void SetClothes(Clothes clothes)
    {
        OnSetClothes?.Invoke(clothes);
    }

    private void SetGenderClothesTypes(GenderClothesTypes genderClothesType)
    {
        _currentGenderClothesType = genderClothesType;

        OnSetGenderClothesType?.Invoke(_currentGenderClothesType.ClothesTypes);
    }

    #region Output

    public event Action<List<ClothesType>> OnSetGenderClothesType;
    public event Action<Clothes> OnSetClothes;

    #endregion
}

public enum ClothesType
{
    None, 
    Woman_Outerwear, Woman_Shoe, Woman_Jewerly, Woman_Glasses, Woman_Hat,
    Man_Outerwear, Man_Shoe, Man_Clock, Man_Beard, Man_Glasses
}
