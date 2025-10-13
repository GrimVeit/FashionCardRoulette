using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StoreCharacterModel
{
    public event Action<List<PersonZero>> OnChooseGender;

    private readonly PersonZeroGroup _group;

    public StoreCharacterModel(PersonZeroGroup group)
    {
        _group = group;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void SelectPersonsByGender(Gender gender)
    {
        var persons = _group.GetPersonsByGender(gender);

        OnChooseGender?.Invoke(persons);
    }
}
