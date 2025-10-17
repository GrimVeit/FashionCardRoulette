using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseShopClothesModel
{
    private readonly IChooseGenderClothesEventsProvider _chooseGenderClothesEventsProvider;
    private readonly IStoreClothesChooseProvider _storeClothesChooseProvider;

    private GenderClothesTypes _genderClothesTypes;

    public ChooseShopClothesModel(IChooseGenderClothesEventsProvider chooseGenderClothesEventsProvider)
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

    private void SetGenderClothesTypes(GenderClothesTypes genderClothesTypes)
    {
        Debug.Log(genderClothesTypes);

        _genderClothesTypes = genderClothesTypes;

        if(_genderClothesTypes == null) return;

        OnChooseGenderClothesTypes?.Invoke(_genderClothesTypes.ClothesTypes);
    }

    public void ChooseType(ClothesType type)
    {
        _storeClothesChooseProvider.ChooseByClothesType(type);
    }

    #region Output

    public event Action<List<ClothesType>> OnChooseGenderClothesTypes;

    #endregion
}
