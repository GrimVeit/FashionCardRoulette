using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChooseGenderClothesModel
{
    private readonly IChooseGenderEventsProvider _chooseGenderEventsProvider;

    private readonly List<GenderClothesTypes> _clothesTypes = new()
    {
        new GenderClothesTypes(Gender.Woman, new List<ClothesType>() { ClothesType.Woman_Outerwear, ClothesType.Woman_Shoe, ClothesType.Woman_Jewerly, ClothesType.Woman_Glasses, ClothesType.Woman_Hat}),
        new GenderClothesTypes(Gender.Man, new List<ClothesType>() {ClothesType.Man_Outerwear, ClothesType.Man_Shoe, ClothesType.Man_Clock, ClothesType.Man_Beard, ClothesType.Man_Glasses})
    };

    public ChooseGenderClothesModel(IChooseGenderEventsProvider chooseGenderEventsProvider)
    {
        _chooseGenderEventsProvider = chooseGenderEventsProvider;
        _chooseGenderEventsProvider.OnChooseGender += SetGender;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _chooseGenderEventsProvider.OnChooseGender -= SetGender;
    }

    private void SetGender(Gender gender)
    {
        var _currentGenderClothesType = _clothesTypes.FirstOrDefault(data => data.Gender == gender);

        if (_currentGenderClothesType == null)
        {
            Debug.LogWarning("Not found GenderClothesType with Gender - " + gender);
            return;
        }

        OnChooseGenderClothesTypes?.Invoke(_currentGenderClothesType);
    }

    #region Output

    public event Action<GenderClothesTypes> OnChooseGenderClothesTypes;

    #endregion
}

public class GenderClothesTypes
{
    public Gender Gender => _gender;
    public List<ClothesType> ClothesTypes => _clothesTypes;

    private Gender _gender;
    private List<ClothesType> _clothesTypes = new List<ClothesType>();

    public GenderClothesTypes(Gender gender, List<ClothesType> clothesTypes)
    {
        _gender = gender;
        _clothesTypes = clothesTypes;
    }

    public bool IsHaveClothesType(ClothesType type)
    {
        return _clothesTypes.Contains(type);
    }
}
